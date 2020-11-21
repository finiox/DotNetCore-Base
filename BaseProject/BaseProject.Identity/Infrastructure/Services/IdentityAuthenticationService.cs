// <copyright file="IdentityAuthenticationService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Identity.Infrastructure.Configuration;
    using BaseProject.Identity.Infrastructure.Database;
    using BaseProject.Identity.Infrastructure.Exceptions;
    using BaseProject.Identity.Infrastructure.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;

    public class IdentityAuthenticationService
    {
        private readonly IdentityConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityAuthenticationService(
            IdentityConfiguration config,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<(string, DateTime)> Login(JwtLoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.LockoutEnabled && user.LockoutEnd > DateTime.Now)
            {
                throw new UserLockedOutException();
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new PasswordIncorrectException();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Jwt.Secret));

            var expirationDate = DateTime.Now.AddMonths(1);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _config.Jwt.ValidIssuer,
                audience: _config.Jwt.ValidAudience,
                expires: expirationDate,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return (token, expirationDate);
        }

        public async Task Login(CMSLoginModel model, string[] requiredRoles)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.LockoutEnabled && user.LockoutEnd > DateTime.Now)
            {
                throw new UserLockedOutException();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var requiredRole in requiredRoles)
            {
                if (!userRoles.Contains(requiredRole))
                {
                    throw new NotInRoleException(requiredRole);
                }
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                throw new PasswordIncorrectException();
            }
        }

        public async Task Logout() => await _signInManager.SignOutAsync();
    }
}
