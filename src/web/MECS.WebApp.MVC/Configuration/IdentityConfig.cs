using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace MECS.WebApp.MVC.Configuration
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(opt =>
                    {
                        opt.LoginPath = "/signin";
                        opt.AccessDeniedPath = "/access-denied";
                    });
        }

    }
}
