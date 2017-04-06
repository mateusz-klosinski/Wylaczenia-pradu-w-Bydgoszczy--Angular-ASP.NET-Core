using Enea.Models;
using Enea.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enea.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<EneaUser> _manager;
        private UserManager<EneaUser> _userManager;

        public AuthController(SignInManager<EneaUser> manager, UserManager<EneaUser> userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string redirectUrl)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _manager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, true, false);

                if (loginResult.Succeeded)
                {
                    if (String.IsNullOrWhiteSpace(redirectUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(redirectUrl);
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Niepoprawna nazwa użytkownika lub hasło");
            }

            return View();
        }


        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _manager.SignOutAsync();
            }

            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newUser = new EneaUser
                {
                    UserName = viewModel.UserName,
                    Email = viewModel.Email
                };

                var creationResult = await _userManager.CreateAsync(newUser, viewModel.Password);

                if (creationResult.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in creationResult.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

    }
}
