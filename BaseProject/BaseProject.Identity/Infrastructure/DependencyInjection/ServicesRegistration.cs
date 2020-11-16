// <copyright file="ServicesRegistration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Identity.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BaseProject.Identity.Infrastructure.Authentication;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesRegistration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<AuthenticationService>();
        }
    }
}
