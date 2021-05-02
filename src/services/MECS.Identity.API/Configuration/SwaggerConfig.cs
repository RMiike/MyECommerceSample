using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace MECS.Identity.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MECS.Identity.API",
                    Version = "v1",
                    Contact = new OpenApiContact() { Name = "Renato Miike Alves", Email = "rmiike.90@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://github.com/RMiike/MyECommerceSample/blob/master/LICENSE") }
                });
            });
        }

        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MECS.Identity.API v1"));
        }
    }
}

