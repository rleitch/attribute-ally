using AttributeAlly.Core.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AttributeAlly.Core.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection HarmonizeSerializationAttributes(this IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = SystemTextJsonContractResolver.Instance
            };
            return services;
        }
    }
}