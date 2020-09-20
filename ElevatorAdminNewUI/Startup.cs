using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Service;
using WebFramework.Configurations;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace ElevatorAdminNewUI
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

            services.AddHttpClient();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.DatabaseConfiguration(Configuration);
            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.SmsConfiguration();
            services.AddSignalR();
            services.ClaimFactoryConfiguration();

            services.AddDistributedMemoryCache();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddSessionStateTempDataProvider();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(1000000);
                //options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });


            services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.FromSeconds(1000000));

            services.AddAuthentication().Services.ConfigureApplicationCookie(options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1000000);
            });

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

            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    //if (context.File.Name.ToLower().EndsWith(".css"))
                    //{
                    //    context.Context.Response.Headers.Append("Cache-Control", "public,max-age=86400");
                    //    context.Context.Response.Headers["Expires"] = "-1";
                    //}
                    //if (context.File.Name.ToLower().EndsWith(".js"))
                    //{
                    //    context.Context.Response.Headers.Append("Cache-Control", "public,max-age=86400");
                    //    context.Context.Response.Headers["Expires"] = "-1";
                    //}
                    //if (context.File.Name.ToLower().EndsWith(".png"))
                    //{
                    //    context.Context.Response.Headers.Append("Cache-Control", "public,max-age=86400");
                    //    context.Context.Response.Headers["Expires"] = "-1";
                    //}
                    // Disable caching of all static files.
                    context.Context.Response.Headers.Add("Cache-Control", "public,max-age=2592000");
                    context.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddDays(30).ToString("R", CultureInfo.InvariantCulture));
                }
            });
            app.UseAuthentication();


            //app.UseSignalR(route =>
            //{
            //    route.MapHub<UserOnlineCountHub>("/");
            //});
            app.UseResponseCaching();
            //app.Use(async (context, next) =>
            //{
            //    //context.Response.Headers[HeaderNames.CacheControl] =
            //    //    new StringValues(new[] { "no-cache", "max-age=2592000", "must-revalidate", "no-store" });
            //    context.Request.Headers.Add("Cache-Control", "public,max-age=2592000");
            //    context.Request.Headers.Append("Expires", DateTime.UtcNow.AddDays(30).ToString("R", CultureInfo.InvariantCulture));
            //    //context.Response.Headers[HeaderNames.Pragma] = "no-cache";

            //    await next();
            //});


            app.UseSession();

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
