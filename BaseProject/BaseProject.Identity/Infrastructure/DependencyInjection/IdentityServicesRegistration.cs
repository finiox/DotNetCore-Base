// <copyright file="IdentityServicesRegistration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BaseProject.Identity.Infrastructure.Authentication;
    using BaseProject.Identity.Infrastructure.Database;
    using BaseProject.Identity.Infrastructure.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class IdentityServicesRegistration
    {
        public static void Register(IServiceCollection services)
        {
            // Services
            services.AddScoped<AuthenticationService>();
            services.AddScoped<UserService>();
        }
    }
}
