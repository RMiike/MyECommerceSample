using MECS.WebApp.MVC.Interfaces;
using MECS.WebApp.MVC.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MECS.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();
        }
    }
}
