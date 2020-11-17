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

                return RedirectToRoute("/");
            }
            catch (NotInRoleException)
            {
                ModelState.AddModelError("not_in_role", "Je hebt geen permissie om in te loggen");
                return View(model);
            }
            catch (UserNotFoundException)
            {
                ModelState.AddModelError("user_not_found", "Gebruiker niet gevonden");
                return View(model);
            }
            catch (PasswordIncorrectException)
            {
                ModelState.AddModelError("password_incorrect", "Wachtwoord onjuist");
                return View(model);
            }
        }
    }
}
