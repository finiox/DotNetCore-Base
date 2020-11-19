// <copyright file="IdentityAccountService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Identity.Infrastructure.Configuration;
    using BaseProject.Identity.Infrastructure.Database;
    using BaseProject.Identity.Infrastructure.Exceptions;
    using BaseProject.Identity.Infrastructure.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class IdentityAccountService
    {
        private readonly IdentityConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityAccountService(
            IdentityConfiguration config,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> CreateAsync(CreateUserModel model)
        {
            static string PinCodeGenerator(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            if (await ExistsAsync(model.UserName))
            {
                throw new UserAlreadyExistsException();
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                PinCode = PinCodeGenerator(_config.Account.PinCodeLength),
                PinCodeAttempts = 0,
                PinCodeGeneration = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new UserCreateException();
            }

            if (model.Roles != null)
            {
                foreach (string role in model.Roles)
                {
                    if (await _roleManager.RoleExistsAsync(role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }
            }

            return user;
        }

        public async Task<ApplicationUser> ConfirmEmail(ConfirmEmailModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.LockoutEnabled && user.LockoutEnd > DateTime.Now)
            {
                throw new UserLockedOutException();
            }

            if (string.IsNullOrEmpty(user.PinCode) || user.EmailConfirmed)
            {
                throw new PinCodeAlreadyConfirmedException();
            }

            if (string.Equals(user.PinCode, model.Pin, StringComparison.OrdinalIgnoreCase))
            {
                user.EmailConfirmed = true;
                user.PinCode = string.Empty;
                user.PinCodeAttempts = 0;
                user.PinCodeGeneration = null;
            }
            else
            {
                user.PinCodeAttempts++;

                if (user.PinCodeAttempts > _config.Account.PinCodeRetries)
                {
                    // Only lockout users who should be locked out
                    if (user.LockoutEnabled)
                    {
                        user.LockoutEnd = DateTime.Now.AddHours(_config.Account.LockoutTimeInHours);
                    }

                    user.PinCodeAttempts = 0;
                }
            }

            await UpdateAsync(user);

            return user;
        }

        public async Task<bool> ExistsAsync(string username) => await _userManager.FindByNameAsync(username) != null;

        public async Task<ApplicationUser> GetAsync(string id) => await _userManager.FindByIdAsync(id);

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync() => await _context.Users.ToListAsync();

        public async Task UpdateAsync(ApplicationUser user) => await _userManager.UpdateAsync(user);

        public async Task DeleteAsync(ApplicationUser user) => await _userManager.DeleteAsync(user);

        public async Task DeleteAsync(string id) => await _userManager.DeleteAsync(await GetAsync(id));

        public async Task Seed()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var users = new List<(ApplicationUser User, string Role)>
                    {
                        (
                            new ApplicationUser
                            {
                                UserName = "admin@finiox.com",
                                Email = "admin@finiox.com",
                            },
                            IdentityRoleService.AdminRole)
                    };

                foreach (var (user, role) in users)
                {
                    var result = await _userManager.CreateAsync(user, "Password12!");

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }
    }
}
