// <copyright file="ServicesRegistration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Common.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Common.Areas.Example.Services;
    using BaseProject.Common.DB;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesRegistration
    {
        public static void Register(IServiceCollection services)
        {
            // DB Context
            services.AddDbContext<BaseProjectContext>();

            // Repositories
            services.AddScoped<ExampleRepository>();
        }
    }
}
