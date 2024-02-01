using AttributeAlly.Core.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AttributeAlly.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection IdkYet(this IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new SystemTextJsonContractResolver()
            };
            return services;
        }
    }
}