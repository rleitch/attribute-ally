using AttributeAlly.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Concurrent;
using System.Reflection;

namespace AttributeAlly.Core.Newtonsoft
{
    public class SystemTextJsonContractResolver : DefaultContractResolver
    {
        public static readonly SystemTextJsonContractResolver Instance = new();

        private static readonly ConcurrentDictionary<MemberInfo, JsonProperty> propertyCache = new();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            return propertyCache.GetOrAdd(member, m =>
            {
                return base.CreateProperty(member, memberSerialization)
                    .HandleJsonPropertyNameAttribute(member)
                    .HandleJsonIgnoreAttribute(member, memberSerialization);
            });
        }
    }
}