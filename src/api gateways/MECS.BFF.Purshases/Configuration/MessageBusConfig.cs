using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MECS.BFF.Purshases.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}