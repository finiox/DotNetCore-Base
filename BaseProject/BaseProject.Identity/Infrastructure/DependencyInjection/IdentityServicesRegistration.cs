// <copyright file="IdentityServicesRegistration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BaseProject.Identity.Infrastructure.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class IdentityServicesRegistration
    {
        public static void AddIdentityProject(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IdentityAuthenticationService>();
            services.AddScoped<IdentityAccountService>();
            services.AddScoped<IdentityRoleService>();
        }
    }
}
