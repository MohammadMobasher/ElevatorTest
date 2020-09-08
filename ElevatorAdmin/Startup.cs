using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Core;
using ElevatorAdmin.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using Service;
using WebFramework.Configurations;

namespace ElevatorAdmin
{
    public class Startup
    {

        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile($"appsettings.json")
            //    .Build());

            services.AddSingleton<HtmlEncoder>(
                HtmlEncoder.Create(allowedRanges: new[]
                {
                    UnicodeRanges.BasicLatin,
                    UnicodeRanges.Arabic
                }));

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddJsonOptions(x =>
                {
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddDistributedMemoryCache();
            services.AddResponseCaching();

            services.AddSession(options =>
            {
                options.CookieName = ".elevator.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(20);
                options.CookieHttpOnly = true;
            });


            //services.AddHttpClient();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".elevator.admin";
                options.Cookie.Expiration = DateTime.Now.Subtract(DateTime.UtcNow).Add(TimeSpan.FromHours(5));

                options.ExpireTimeSpan = DateTime.Now.Subtract(DateTime.UtcNow).Add(TimeSpan.FromHours(5));

                options.SlidingExpiration = true;
            });


            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = DateTime.Now.Subtract(DateTime.UtcNow).Add(TimeSpan.FromHours(5));
                //options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });


            services.DatabaseConfiguration(Configuration);
            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.SmsConfiguration();
            services.AddSignalR();
            services.ClaimFactoryConfiguration();

            services.AddDistributedMemoryCache();



            services.AddMvc();
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //    .AddSessionStateTempDataProvider();





            //services.Configure<SecurityStampValidatorOptions>(options =>
            //{
            //    options.ValidationInterval = TimeSpan.FromMinutes(1000000)
            //});

            



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            // Redirect StatusCode 400 & 404
            app.UseRedirectConfigure();
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseAuthentication();


            //app.UseSignalR(route =>
            //{
            //    route.MapHub<UserOnlineCountHub>("/");
            //});

            app.UseSession(new SessionOptions() { Cookie = new CookieBuilder() { Name = ".AspNetCore.Session.ElevatorAdmin" } });

            //app.UseSession();

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
