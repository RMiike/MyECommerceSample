using MECS.Core.Domain.Entities;
using MECS.WebAPI.Core.Identity;
using MECS.WebAPI.Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MECS.BFF.Purshases.Configuration
{
    public static class APIConfig
    {
        public static void AddAPIConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers();
            services.Configure<AppSettings>(configuration);
            services.AddCors(opt =>
            {
                opt.AddPolicy("Total",
                    builder =>
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyHeader());
            });
        }

        public static void UseAPIConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var apiName = "Bff Compras";
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
