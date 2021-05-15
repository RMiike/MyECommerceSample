using MECS.Client.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace MECS.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ClientContext>();

        }
    }
}
