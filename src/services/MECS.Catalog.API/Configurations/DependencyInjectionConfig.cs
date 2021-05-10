using MECS.Catalog.API.Data;
using MECS.Catalog.API.Interfaces;
using MECS.Catalog.API.Repositories;
using MECS.Catalog.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MECS.Catalog.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<CatalogContext>();
        }
    }
}
