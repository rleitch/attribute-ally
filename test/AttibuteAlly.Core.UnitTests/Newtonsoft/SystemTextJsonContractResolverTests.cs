using AttibuteAlly.Core.UnitTests.TestObjects;
using AttributeAlly.Core.Newtonsoft;
using Newtonsoft.Json;
using Xunit;

namespace AttibuteAlly.Core.UnitTests.Newtonsoft
{
    public class SystemTextJsonContractResolverTests
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public SystemTextJsonContractResolverTests()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = SystemTextJsonContractResolver.Instance
            };
        }

        [Theory]
        [InlineData("json-property-123")]
        [InlineData("json-property-12")]
        [InlineData("data-member-13")]
        [InlineData("json-property-23")]
        [InlineData("data-member-1")]
        [InlineData("json-property-2")]
        [InlineData("json-property-name-3")]
        public void SerializeObject_WhenInputDataContract_ShouldPrioritizeNewtonsoft(string expectedSubstring)
        {
            // Arrange
            var testClass = new DataContractClass();

            // Act
            string serialized = JsonConvert.SerializeObject(testClass, _jsonSerializerSettings);


            // Assert
            Assert.Contains(expectedSubstring, serialized);
        }

        [Theory]
        [InlineData("json-property-123")]
        [InlineData("json-property-12")]
        [InlineData("json-property-name-13")]
        [InlineData("json-property-23")]
        [InlineData("One")]
        [InlineData("json-property-2")]
        [InlineData("json-property-name-3")]
        [InlineData("Zero")]
        public void SerializeObject_WhenInputClassNotDecorated_ShouldPrioritizeNewtonsoft(string expectedSubstring)
        {
            // Arrange
            var testClass = new NormalClass();

            // Act
            string serialized = JsonConvert.SerializeObject(testClass, _jsonSerializerSettings);


            // Assert
            Assert.Contains(expectedSubstring, serialized);
        }
    }
}