using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AttibuteAlly.Core.UnitTests.TestObjects
{
    public class NormalClass
    {
        [DataMember(Name = "data-member-123")]
        [JsonProperty("json-property-123")]
        [JsonPropertyName("json-property-name-123")]
        public int OneTwoThree { get; set; } = 123;

        [DataMember(Name = "data-member-12")]
        [JsonProperty("json-property-12")]
        public int OneTwo { get; set; } = 12;

        [DataMember(Name = "data-member-13")]
        [JsonPropertyName("json-property-name-13")]
        public int OneThree { get; set; } = 13;

        [JsonProperty("json-property-23")]
        [JsonPropertyName("json-property-name-23")]
        public int TwoThree { get; set; } = 23;

        [DataMember(Name = "data-member-1")]
        public int One { get; set; } = 1;

        [JsonProperty("json-property-2")]
        public int Two { get; set; } = 2;

        [JsonPropertyName("json-property-name-3")]
        public int Three { get; set; } = 3;

        public int Zero { get; set; } = 0;
    }
}