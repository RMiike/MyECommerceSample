using Microsoft.Extensions.Configuration;

namespace MECS.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetMessageQueueConnection(this IConfiguration configuration, string name)
            => configuration?.GetSection("MessageQueueConnection")?[name];
    }
}
