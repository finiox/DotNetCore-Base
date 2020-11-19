// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.API.Infrastructure.Configuration;
    using BaseProject.API.Infrastructure.Factories;
    using BaseProject.Common.DB;
    using BaseProject.Common.Infrastructure.Configuration;
    using BaseProject.Common.Infrastructure.DependencyInjection;
    using BaseProject.Identity.Infrastructure.Configuration;
    using BaseProject.Identity.Infrastructure.Database;
    using BaseProject.Identity.Infrastructure.DependencyInjection;
    using BaseProject.Identity.Infrastructure.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        private readonly APIConfiguration _config;
        private readonly IdentityConfiguration _identityConfig;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _config = configuration.Get<APIConfiguration>();
            _identityConfig = configuration.Get<IdentityConfiguration>();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseProject", Version = "v1" });
            });

            // Add config singleton
            services.AddSingleton(_config);
            services.AddSingleton<AppConfiguration>(_config);
            services.AddSingleton(_identityConfig);

            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext => ModelStateFactory.InvalidResponse(actionContext);
                });

            // Identity project setup
            services.AddIdentityProject(
                dbOptions => dbOptions.UseSqlServer(_config.DB.ConnectionString),
                identityOptions =>
                {
                    // Password settings
                    identityOptions.Password.RequireDigit = true;
                    identityOptions.Password.RequiredLength = 8;
                    identityOptions.Password.RequireNonAlphanumeric = false;
                    identityOptions.Password.RequireUppercase = true;
                    identityOptions.Password.RequireLowercase = false;
                    identityOptions.Password.RequiredUniqueChars = 6;

                    // Lockout settings
                    identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                    identityOptions.Lockout.AllowedForNewUsers = true;

                    // User settings
                    identityOptions.User.RequireUniqueEmail = true;
                });

            services.AddCommonProject();

            // Authentication
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidAudience = _identityConfig.Jwt.ValidAudience,
                        ValidIssuer = _identityConfig.Jwt.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityConfig.Jwt.Secret)),
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseProject v1"));

                // Migrate Database in development only
                MigrateDatabase(app.ApplicationServices);
                SeedData(app.ApplicationServices).GetAwaiter().GetResult();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<BaseProjectContext>();

            context.Database.Migrate();
        }

        private static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();

            var roleService = scope.ServiceProvider.GetService<IdentityRoleService>();
            var accountService = scope.ServiceProvider.GetService<IdentityAccountService>();

            await roleService.Seed();
            await accountService.Seed();
        }
    }
}
