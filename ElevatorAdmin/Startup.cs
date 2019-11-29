﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service;
using WebFramework.Configurations;

namespace ElevatorAdmin
{
    public class Startup
    {
        
        private readonly SiteSettings _siteSetting;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.DatabaseConfiguration(Configuration);
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = ".Elevator.Cookies";
            }).AddCookie(".Elevator.Cookies", options =>
            {
                options.Cookie.Name = ".Elevator.auth";
                options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = new PathString("/account/Login");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(300000);
          
            });

            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.SmsConfiguration();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Redirect StatusCode 400 & 404
            app.UseRedirectConfigure();

            app.UseStaticFiles();
            app.UseCookiePolicy();
          
            app.UseAuthentication();



            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                routes.MapRoute(
                  name: "MyArea",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}