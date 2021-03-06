// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.CMS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseProject.CMS.Infrastructure.Configuration;
    using BaseProject.Common.DB;
    using BaseProject.Common.Infrastructure.Configuration;
    using BaseProject.Common.Infrastructure.DependencyInjection;
    using BaseProject.Identity.Infrastructure.Configuration;
    using BaseProject.Identity.Infrastructure.Database;
    using BaseProject.Identity.Infrastructure.DependencyInjection;
    using BaseProject.Identity.Infrastructure.Services;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly CMSConfiguration _config;
        private readonly IdentityConfiguration _identityConfig;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _config = configuration.Get<CMSConfiguration>();
            _identityConfig = configuration.Get<IdentityConfiguration>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add config singleton
            services.AddSingleton(_config);
            services.AddSingleton<AppConfiguration>(_config);
            services.AddSingleton(_identityConfig);

            // Identity project setup
            services.AddIdentityProject(
                dbOptions => dbOptions.UseSqlServer(_config.DB.ConnectionString),
                identityOptions =>
                {
                    // User Settings
                    identityOptions.User.RequireUniqueEmail = true;
                });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCommonProject(options =>
                options.UseSqlServer(_config.DB.ConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Migrate Database in development only
                MigrateDatabase(app.ApplicationServices);
                SeedData(app.ApplicationServices).GetAwaiter().GetResult();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area=Home}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
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
