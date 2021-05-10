using MECS.Identity.API.Data;
using MECS.Identity.API.Extensions;
using MECS.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MECS.Identity.API.Configuration
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                    .AddErrorDescriber<IdentityMensagensPortugues>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddJWTConfiguration(configuration);
        }
    }
}
