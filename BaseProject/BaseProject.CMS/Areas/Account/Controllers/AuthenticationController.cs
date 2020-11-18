// <copyright file="AuthenticationController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.CMS.Areas.Account.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseProject.CMS.Areas.Account.ViewModels;
    using BaseProject.Identity.Infrastructure.Authentication;
    using BaseProject.Identity.Infrastructure.Exceptions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Account")]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Login() => View(new LoginPageModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginPageModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _authenticationService.Login(
                    new ()
                    {
                        UserName = model.Email,
                        Password = model.Password,
                        RememberMe = model.RememberMe
                    },
                    new string[] { "Admin" });

                return Redirect("/");
            }
            catch (NotInRoleException)
            {
                ModelState.AddModelError(string.Empty, "You do not have sufficient permission.");
                return View(model);
            }
            catch (UserNotFoundException)
            {
                ModelState.AddModelError("Email", "User not found.");
                return View(model);
            }
            catch (PasswordIncorrectException)
            {
                ModelState.AddModelError("Password", "Password is incorrect.");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();

            return RedirectToAction(nameof(Login));
        }
    }
}
