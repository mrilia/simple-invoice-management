using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SIM.Api.Middleware;
using SIM.Application.AutoMapper;
using SIM.Application.Interfaces;

namespace SIM.Api.Installer
{
    public class MvcInstaller : IInstaller

    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddCors();

            services.AddControllers(opt => opt.Filters.Add<OnExceptionMiddleware>())
                .AddFluentValidation(mvcConfiguration =>
                    mvcConfiguration.RegisterValidatorsFromAssemblyContaining<ISIMContext>());

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Simple Invoice Manager API",
                    Version = "v1.0",
                    Description = "Simple Invoice Management ASP.NET Core Web API",
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

                
            });

        }
    }
}