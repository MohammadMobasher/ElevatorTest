using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Configurations
{
    public static class AppSettingFoTagHelperExtension
    {
        public static void AddAppSettingFoTagHelper(this IServiceCollection services, AppSettingForTagHelper config)
        {
            services.AddSingleton(_ => config);
        }
    }
}
