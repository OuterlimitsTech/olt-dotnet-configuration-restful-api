using Microsoft.Extensions.Configuration;
using Moq;

namespace OLT.Extensions.Configuration.RESTApi.Tests
{
    public class ConfigurationsUnitTest
    {
        [Fact]
        public void BuildConfiguration_ShouldReturnConfigurationSource()
        {
            var configRoot = BuildMockOptions();
            var mockConfigBuilder = new Mock<IConfigurationBuilder>();
            mockConfigBuilder.Setup(b => b.Build()).Returns(configRoot);

            var configBuilder = mockConfigBuilder.Object;
            var configSection = configBuilder.Build().GetSection("ConfigApi");

            var endpoint = configSection["Endpoint"] ?? string.Empty;
            var environment = configSection["Environment"] ?? string.Empty;
            var apiKey = configSection["ApiKey"] ?? string.Empty;

            ConfigurationBuilderExtensions.AddRestApiConfigProvider(configBuilder, endpoint, true, options =>
            {
                options
                    .SetHeaderAcceptJson()
                    .SetQueryParam("env", environment)  //Environment
                    .UseAuthentication(auth => auth.WithApiKey(apiKey, "API-KEY"));

            });

            var result = configBuilder.Build();
            Assert.NotNull(result);            
            Assert.IsAssignableFrom<IConfiguration>(result);

            
            var source = result.Get<RestApiProviderConfigurationSource>();

            Assert.NotNull(source);
            Assert.IsAssignableFrom<RestApiProviderConfigurationSource>(source);
        }


        private static IConfigurationRoot BuildMockOptions()
        {
            var dict = new Dictionary<string, string?>
            {
                { "ConfigApi:Endpoint", Faker.Internet.SecureUrl() },
                { "ConfigApi:Environment", Faker.Internet.DomainName() },
                { "ConfigApi:ApiKey", Faker.Lorem.Words(3).Last() },
                { "ConfigApi:Token", Faker.Lorem.Words(4).Last() },
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(dict)
                .Build();
        }
    }
}