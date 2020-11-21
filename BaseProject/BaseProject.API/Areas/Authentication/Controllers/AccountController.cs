// <copyright file="AccountController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.API.Areas.Authentication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseProject.API.Areas.Authentication.ViewModels;
    using BaseProject.API.Shared.ViewModels;
    using BaseProject.Identity.Infrastructure.Exceptions;
    using BaseProject.Identity.Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IdentityAccountService _accountService;

        public AccountController(IdentityAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
        {
            try
            {
                var user = await _accountService.CreateAsync(new ()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password
                });

                // TODO: Send confirm email mail
                return Ok();
            }
            catch (UserAlreadyExistsException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    ErrorKey = "user_already_exists"
                });
            }
            catch (UserCreateException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    ErrorKey = "user_could_not_be_created"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorViewModel.UNHANDLED_EXCEPTION);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequestModel model)
        {
            try
            {
                var user = await _accountService.ConfirmEmail(new ()
                {
                    Email = model.Email,
                    Pin = model.Pin
                });

                if (user.EmailConfirmed == false)
                {
                    return BadRequest(new ErrorViewModel()
                    {
                        ErrorKey = "pin_incorrect"
                    });
                }

                return Ok();
            }
            catch (UserNotFoundException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    ErrorKey = "username_not_found"
                });
            }
            catch (UserLockedOutException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    ErrorKey = "user_locked_out"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorViewModel.UNHANDLED_EXCEPTION);
            }
        }
    }
}
