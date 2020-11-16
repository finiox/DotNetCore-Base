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
    using BaseProject.API.Infrastructure.Configuration;
    using BaseProject.API.Shared.ViewModels;
    using BaseProject.Identity.Infrastructure.Authentication;
    using BaseProject.Identity.Infrastructure.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly APIConfiguration _config;

        public AuthenticationController(AuthenticationService authenticationService, APIConfiguration config)
        {
            _authenticationService = authenticationService;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            try
            {
                string token = await _authenticationService.Login(
                    new ()
                    {
                        UserName = model.Email,
                        Password = model.Password
                    },
                    _config.Jwt);

                return Ok(token);
            }
            catch (UserNotFoundException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    ErrorKey = "username_not_found"
                });
            }
            catch (PasswordIncorrectException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    ErrorKey = "password_incorrect"
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
