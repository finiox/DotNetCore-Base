// <copyright file="CommonServicesRegistration.cs" company="PlaceholderCompany">
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
    using BaseProject.Common.Infrastructure.Files;
    using Microsoft.Extensions.DependencyInjection;

    public static class CommonServicesRegistration
    {
        public static void AddCommonProject(this IServiceCollection services)
        {
            // DB Context
            services.AddDbContext<BaseProjectContext>();

            // Repositories
            services.AddScoped<ExampleRepository>();

            // Services
            services.AddScoped<ExampleService>();
            services.AddScoped<FileService>();
        }
    }
}
