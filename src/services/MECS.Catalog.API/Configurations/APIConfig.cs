﻿using MECS.Catalog.API.Data;
using MECS.WebAPI.Core.Identity;
using MECS.WebAPI.Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MECS.Catalog.API.Configurations
{
    public static class APIConfig
    {
        public static void AddAPIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<CatalogContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(opt =>
            {
                opt.AddPolicy("Total",
                    builder =>
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyHeader());
            });

        }
        public static void UseAPIConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var apiName = "Identity";
                app.UseSwaggerConfiguration(apiName);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseIdentityConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
