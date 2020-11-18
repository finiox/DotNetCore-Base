// <copyright file="IdentityRoleService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Identity.Infrastructure.Database;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class IdentityRoleService
    {
        public const string AdminRole = "Admin";

        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityRoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                var rolesToAdd = new List<IdentityRole>
                {
                    new IdentityRole(AdminRole)
                };

                foreach (var role in rolesToAdd)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }
    }
}
