using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AttributeAlly.Core.Extensions
{
    public static class JsonPropertyExtensions
    {
        public static JsonProperty HandleJsonPropertyNameAttribute(this JsonProperty property, MemberInfo member)
        {
            if (!member.IsDefined(typeof(JsonPropertyAttribute), true)
                    && (member.DeclaringType?.GetCustomAttribute<DataContractAttribute>() == null
                        || !member.IsDefined(typeof(DataMemberAttribute), true)))
            {
                var jsonPropertyNameAttribute = member.GetCustomAttribute<JsonPropertyNameAttribute>();
                if (jsonPropertyNameAttribute != null)
                {
                    property.Ignored = false;
                    property.PropertyName = jsonPropertyNameAttribute.Name;
                }
            }
            return property;
        }

        public static JsonProperty HandleJsonIgnoreAttribute(this JsonProperty property, MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonIgnoreAttribute = member.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
            if (jsonIgnoreAttribute != null)
            {
                switch (jsonIgnoreAttribute.Condition)
                {
                    case JsonIgnoreCondition.Never:
                        property.Ignored = false;
                        break;
                    case JsonIgnoreCondition.Always:
                        property.Ignored = true;
                        break;
                    case JsonIgnoreCondition.WhenWritingDefault:
                        if (memberSerialization == MemberSerialization.OptOut)
                        {
                            property.ShouldSerialize = instance =>
                            {
                                var value = property.ValueProvider.GetValue(instance);
                                return value != null && !value.Equals(property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null);
                            };
                        }
                        break;
                    case JsonIgnoreCondition.WhenWritingNull:
                        property.NullValueHandling = NullValueHandling.Ignore;
                        break;
                }
            }
            return property;
        }
    }
}