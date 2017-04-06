using Enea.Models;
using Enea.Services;
using Enea.Services.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enea.Controllers.api
{
    [Route("/api/schedule/fire")]
    public class ScheduleController : Controller
    {
        private EneaContext _context;
        private DisconnectionService _disconnectionService;
        private MailService _mailService;

        public ScheduleController(EneaContext context, DisconnectionService disconnectionService, MailService mailService)
        {
            _disconnectionService = disconnectionService;
            _mailService = mailService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CheckDisconnections()
        {
            var disconnections = await _disconnectionService.DownloadAndFormatDisconnectionsIntoListAsync();

            var users = await _context.Users
                .Include(u => u.KeyWords)
                .Where(u => u.HasActiveSubscription && u.KeyWords.Any())
                .ToListAsync();

            List<MimeMessage> generatedEmails = new List<MimeMessage>();

            if (disconnections.Tommorow.Any() && users.Any())
            {
                foreach (var user in users)
                {
                    var matchingDisconnections = new List<Disconnection>();
                    var matchingWords = new List<string>();

                    var userKeyWords = user.KeyWords
                        .Where(uk => uk.IsOnline)
                        .ToList();

                    foreach (var disconnection in disconnections.Tommorow)
                    {
                        foreach (var keyword in userKeyWords)
                        {
                           if (disconnection.Area.ToLower().Contains(keyword.Word.ToLower()) ||
                                disconnection.Details.ToLower().Contains(keyword.Word.ToLower()))
                            {
                                matchingDisconnections.Add(disconnection);

                                if (!matchingWords.Contains(keyword.Word))
                                    matchingWords.Add(keyword.Word);
                            }
                        }
                    }


                    if (matchingDisconnections.Any())
                    {
                        generatedEmails.Add(_mailService.GenerateEmail(user.Email,
                            "Przerwy w dostawie prądu w Twojej okolicy!",
                            _mailService.GenerateEmailBody(matchingDisconnections, matchingWords)));
                        user.LastNotificationSent = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                }

                if (generatedEmails.Any())
                {
                    _mailService.SendEmail(generatedEmails);
                }

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
