// <copyright file="UserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Identity.Infrastructure.Database;
    using BaseProject.Identity.Infrastructure.Exceptions;
    using BaseProject.Identity.Infrastructure.Services.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> CreateAsync(CreateUserModel model)
        {
            if (await ExistsAsync(model.UserName))
            {
                throw new UserAlreadyExistsException();
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
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

        public async Task<bool> ExistsAsync(string username) => await _userManager.FindByNameAsync(username) != null;

        public async Task<ApplicationUser> GetAsync(string id) => await _userManager.FindByIdAsync(id);

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync() => await _context.Users.ToListAsync();

        public async Task UpdateAsync(ApplicationUser user) => await _userManager.UpdateAsync(user);

        public async Task DeleteAsync(ApplicationUser user) => await _userManager.DeleteAsync(user);

        public async Task DeleteAsync(string id) => await _userManager.DeleteAsync(await GetAsync(id));
    }
}
