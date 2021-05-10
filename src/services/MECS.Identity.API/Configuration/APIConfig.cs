using MECS.WebAPI.Core.Identity;
using MECS.WebAPI.Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MECS.Identity.API.Configuration
{
    public static class APIConfig
    {
        public static void AddAPIConfig(this IServiceCollection services)
        {
            services.AddControllers();
        }

        public static void UseAPIConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var apiName = "Identity";
                app.UseSwaggerConfiguration(apiName);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseIdentityConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
