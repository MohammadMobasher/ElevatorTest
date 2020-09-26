using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core;
using ElevatorNewUI.Controllers;
using IHostedServiceSample;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Service;
using Service.Mappers;
using Service.Repos;
using WebFramework.Configurations;

namespace ElevatorNewUI
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

            services.AddAppSettingFoTagHelper(new AppSettingForTagHelper(_siteSetting.GoogleReCaptcha));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.DatabaseConfiguration(Configuration);
            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.FromSeconds(10));

            services.AddAuthentication().Services.ConfigureApplicationCookie(options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(40);
                options.Cookie.Expiration = TimeSpan.FromMinutes(40);

            });

            #region Mapper
            services.AddAutoMapper(typeof(SlideShowMapper));
            #endregion

            services.Configure<CookieTempDataProviderOptions>(options => {
                options.Cookie.IsEssential = true;
            });

            services.SmsConfiguration();
            services.ClaimFactoryConfiguration();
            services.AddScoped<ManageBankService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
             .AddSessionStateTempDataProvider();
            services.AddResponseCaching();



            //services.AddScoped<HostedService, DeleteOrderService>();
            //services.AddScoped<IHostedService>();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                //options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {

            app.UseDeveloperExceptionPage();


            app.UseRedirectConfigure();
            app.UseResponseCaching();
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
                    if (context.File.Name.ToLower().EndsWith(".png"))
                    {
                        context.Context.Response.Headers.Append("Cache-Control", "public,max-age=86400");
                        context.Context.Response.Headers["Expires"] = "-1";
                    }
                    // Disable caching of all static files.
                    context.Context.Response.Headers.Add("Cache-Control", "public,max-age=2592000");
                    context.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddDays(30).ToString("R", CultureInfo.InvariantCulture));
                }
            });

            //app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    OnPrepareResponse = ctx =>
            //    {
            //        const int durationInSeconds = 60 * 60 * 24 * 365;
            //        ctx.Context.Response.Headers[HeaderNames.CacheControl] =
            //            "public,max-age=" + durationInSeconds;
            //    }
            //});
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseSession();






            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
