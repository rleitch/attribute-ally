using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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
                var property = base.CreateProperty(member, memberSerialization);
                if (!m.IsDefined(typeof(JsonPropertyAttribute), true)
                    && (m.DeclaringType?.GetCustomAttribute<DataContractAttribute>() == null
                        || !m.IsDefined(typeof(DataMemberAttribute), true)))
                {
                    var jsonPropertyNameAttribute = m.GetCustomAttribute<JsonPropertyNameAttribute>();
                    if (jsonPropertyNameAttribute != null)
                    {
                        property.Ignored = false;
                        property.PropertyName = jsonPropertyNameAttribute.Name;
                    }
                }
                return property;
            });
        }
    }
}