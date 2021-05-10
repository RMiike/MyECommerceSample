using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace MECS.WebAPI.Core.Swagger
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, string apiName, bool haveToAuthorize)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"MECS.{apiName}.API",
                    Version = "v1",
                    Contact = new OpenApiContact() { Name = "Renato Miike Alves", Email = "rmiike.90@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://github.com/RMiike/MyECommerceSample/blob/master/LICENSE") }
                });

                if (haveToAuthorize)
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                        Name = "Authorization",
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string [] { }
                        }
                    });
                }
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app, string apiName)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"MECS.{apiName}.API v1"));
        }
    }
}
