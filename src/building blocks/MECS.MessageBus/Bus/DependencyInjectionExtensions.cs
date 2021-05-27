using Microsoft.Extensions.DependencyInjection;
using System;

namespace MECS.MessageBus.Bus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string stringConnection)
        {
            if (string.IsNullOrEmpty(stringConnection))
                throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(stringConnection));
            return services;
        }
    }
}
