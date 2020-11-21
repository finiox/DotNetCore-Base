// <copyright file="AuthenticationController.cs" company="PlaceholderCompany">
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
    public class AuthenticationController : ControllerBase
    {
        private readonly IdentityAuthenticationService _authenticationService;

        public AuthenticationController(IdentityAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel model)
        {
            try
            {
                var (token, expirationDate) = await _authenticationService
                    .Login(new ()
                    {
                        UserName = model.Email,
                        Password = model.Password
                    });

                return Ok(new LoginResponseModel()
                {
                    Token = token,
                    ExpirationDate = expirationDate
                });
            }
            catch (UserNotFoundException)
            {
                return BadRequest(ErrorViewModel.NOT_FOUND);
            }
            catch (PasswordIncorrectException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    ErrorKey = "password_incorrect"
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
