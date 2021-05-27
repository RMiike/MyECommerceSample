
using MECS.Client.API.Services;
using MECS.Core.Extensions;
using MECS.MessageBus.Bus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MECS.Client.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegisterClientIntegrationHandler>();
        }
    }
}