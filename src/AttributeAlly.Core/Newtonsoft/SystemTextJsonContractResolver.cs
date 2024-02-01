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

        private static readonly ConcurrentDictionary<MemberInfo, string> propertyNameCache = new();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var hasDataContractAttribute = member.DeclaringType?.GetCustomAttribute<DataContractAttribute>() != null;

            if(member.IsDefined(typeof(JsonPropertyAttribute), true))
            {
                return property;
            }

            if (hasDataContractAttribute && member.IsDefined(typeof(DataMemberAttribute), true))
            {
                return property;
            }

            var jsonPropertyNameAttribute = member.GetCustomAttribute<JsonPropertyNameAttribute>();
            if(jsonPropertyNameAttribute != null)
            {
                property.Ignored = false;
                property.PropertyName = jsonPropertyNameAttribute.Name;
            }

            return property;
        }
    }
}