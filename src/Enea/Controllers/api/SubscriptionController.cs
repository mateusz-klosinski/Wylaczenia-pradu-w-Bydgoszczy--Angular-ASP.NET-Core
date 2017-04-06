using AutoMapper;
using Enea.Models;
using Enea.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Enea.Services.Email;

namespace Enea.Controllers.api
{
    [Authorize]
    [Route("api/subscription")]
    public class SubscriptionController : Controller
    {
        private EneaContext _context;

        public SubscriptionController(EneaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userName = User.Identity.Name;


            var subscriptionData = _context.Users
                .Include(u => u.KeyWords)
                .Where(u => u.UserName == userName)
                .Select(u => new SubscriptionViewModel
                {
                    HasActiveSubscription = u.HasActiveSubscription,
                    LastNotificationSent = u.LastNotificationSent,
                    KeyWords = u.KeyWords.Where(k => k.IsOnline).ToList(),
                })
                .FirstOrDefault();
            return Ok(subscriptionData);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SubscriptionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;

                var user = _context.Users
                    .Where(u => u.UserName == userName)
                    .FirstOrDefault();

                user.HasActiveSubscription = viewModel.HasActiveSubscription;

                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
        
    }
}
