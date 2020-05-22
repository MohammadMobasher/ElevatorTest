using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core;
using ElevatorNewUI.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Service;
using Service.Mappers;
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.DatabaseConfiguration(Configuration);
            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.AddAuthentication();

            #region Mapper
            services.AddAutoMapper(typeof(SlideShowMapper));
            #endregion

            services.SmsConfiguration();
            services.ClaimFactoryConfiguration();
            services.AddScoped<ManageBankService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseDeveloperExceptionPage();


            app.UseRedirectConfigure();

            app.UseStaticFiles();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
