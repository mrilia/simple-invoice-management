using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIM.Application.Interfaces;
using SIM.Persistence.Context;

namespace SIM.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SIMContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SIMContext"),sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        10,
                        TimeSpan.FromSeconds(30),
                        null);
                });
                options.EnableSensitiveDataLogging();
                
                
            });

            services.AddScoped<ISIMContext>(provider => provider.GetService<SIMContext>());

            return services;
        }
    }
}