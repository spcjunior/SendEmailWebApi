using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SendEmail.WebApi.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ConfigurationExtension
    {
        public static T SafeGet<T>(this IConfiguration configuration)
        {
            var typeName = typeof(T).Name;

            if (configuration.GetChildren().Any(item => item.Key == typeName))
            {
                return configuration.GetSection(typeName).Get<T>();
            }

            return configuration.Get<T>();
        }
    }
}
