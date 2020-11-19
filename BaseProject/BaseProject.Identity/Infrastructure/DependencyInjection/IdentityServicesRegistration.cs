// <copyright file="IdentityServicesRegistration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BaseProject.Identity.Infrastructure.Database;
    using BaseProject.Identity.Infrastructure.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class IdentityServicesRegistration
    {
        public static void AddIdentityProject(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> dbContextOptions = null,
            Action<IdentityOptions> identityOptions = null)
        {
            // Entity Framework
            services
                .AddDbContext<ApplicationDbContext>(dbContextOptions);

            // Identity
            services
                .AddIdentity<ApplicationUser, IdentityRole>(identityOptions)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Services
            services.AddScoped<IdentityAuthenticationService>();
            services.AddScoped<IdentityAccountService>();
            services.AddScoped<IdentityRoleService>();
        }
    }
}
