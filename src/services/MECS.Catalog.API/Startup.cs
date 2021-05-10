using MECS.Catalog.API.Configurations;
using MECS.WebAPI.Core.Identity;
using MECS.WebAPI.Core.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MECS.Catalog.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsetings.{environment.EnvironmentName}.json", true, true);
            if (environment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAPIConfiguration(Configuration);

            services.AddJWTConfiguration(Configuration);

            services.AddDependencyInjectionConfiguration();

            var apiName = "Catalog";
            services.AddSwaggerConfiguration(apiName, true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseAPIConfiguration(env);
        }
    }
}
