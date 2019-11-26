using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestEntity
{
    public static class Config
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContexts>
                (options => options.UseSqlServer(configuration.GetConnectionString("MyConnection")), ServiceLifetime.Scoped);

            services.AddScoped<DbContext>();
        }
    }
}
