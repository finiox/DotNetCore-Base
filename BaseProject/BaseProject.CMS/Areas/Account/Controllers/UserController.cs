// <copyright file="UserController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.CMS.Areas.Account.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseProject.CMS.Areas.Account.ViewModels;
    using BaseProject.Identity.Infrastructure.Exceptions;
    using BaseProject.Identity.Infrastructure.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Area("Account")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IdentityAccountService _userService;

        public UserController(IdentityAccountService userService)
        {
            _userService = userService;
        }

        public IActionResult Index() => RedirectToAction(nameof(Overview));

        public async Task<IActionResult> Overview(bool success = false)
        {
            ViewBag.ShowSuccessDialog = success;

            return View(new UserOverviewPageModel()
            {
                Users = await _userService.GetAllAsync()
            });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
            {
                return RedirectToAction(nameof(Overview));
            }

            return View(new UserEditPageModel(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromForm] UserEditPageModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userService.GetAsync(model.Id);

            if (user == null)
            {
                return RedirectToAction(nameof(Overview));
            }

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;

            try
            {
                await _userService.UpdateAsync(user);

                return RedirectToAction(nameof(Overview), new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        public IActionResult Create() => View(new UserCreatePageModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserCreatePageModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var roles = model.MakeAdmin ? new[] { "Admin" } : null;

                var user = await _userService.CreateAsync(new ()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password,
                    Roles = roles
                });

                return RedirectToAction(nameof(Edit), new { id = user.Id });
            }
            catch (UserAlreadyExistsException)
            {
                ModelState.AddModelError("Email", "User with this email adress already exists");
                return View(model);
            }
            catch (UserCreateException)
            {
                ModelState.AddModelError(string.Empty, "User could not be created");
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] UserDeleteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Overview));
            }

            try
            {
                await _userService.DeleteAsync(model.Id);

                return RedirectToAction(nameof(Overview), new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
