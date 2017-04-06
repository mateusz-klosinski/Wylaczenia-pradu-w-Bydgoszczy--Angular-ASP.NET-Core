using Enea.Models;
using Enea.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enea.Controllers.api
{
    [Authorize]
    [Route("/api/settings")]
    public class AuthController : Controller
    {
        private EneaContext _context;
        private UserManager<EneaUser> _manager;

        public AuthController(UserManager<EneaUser> manager, EneaContext context)
        {
            _manager = manager;
            _context = context;
        }

        [HttpPost("setNewCredentials")]
        public async Task<IActionResult> SetCredentials([FromBody] SettingsViewModel viewModel)
        {
            var userName = User.Identity.Name;

            var user = _context.Users
                .Where(u => u.UserName == userName)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(viewModel.Email))
            {
                user.Email = viewModel.Email;
                await _manager.UpdateNormalizedEmailAsync(user);
                await _context.SaveChangesAsync();
            }
            if (!string.IsNullOrEmpty(viewModel.Password))
            {
                await _manager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.Password);
            }
            if (!string.IsNullOrEmpty(viewModel.UserName))
            {

                user.UserName = viewModel.UserName;
                await _manager.UpdateNormalizedUserNameAsync(user);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
