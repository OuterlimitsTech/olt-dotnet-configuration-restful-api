using Microsoft.Extensions.Configuration;
using Moq;

namespace OLT.Extensions.Configuration.REST.Api.Tests
{
    public class ConfigurationsUnitTest
    {
        [Fact]
        public void BuildConfiguration_ShouldReturnIConfiguration()
        {
            // Arrange
            var mockConfigurationBuilder = new Mock<IConfigurationBuilder>();
            var mockConfiguration = new Mock<IConfiguration>();
            //mockConfigurationBuilder.Setup(x => x.Build()).Returns(mockConfiguration.Object);

            ConfigurationBuilderExtensions.AddRestApiConfigProvider(mockConfigurationBuilder, "https://test.com", false, options =>
            {
                options
                    .SetHeaderAcceptJson()
                    .SetQueryParam("env", "dev")                    
                    .UseAuthentication(auth => auth.WithApiKey("1234", "API-KEY"))
                    ;

            });
            //var service = new ConfigurationService(mockConfigurationBuilder.Object);

            // Act
            var result = service.BuildConfiguration();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IConfiguration>(result);
        }

        [Fact]
        public void TestOptions()
        {
            var optionsBuilder = new ApiOptionsBuilder("https://fake.com");

            optionsBuilder                
                .SetHeaderAcceptJson()
                .UseAuthentication(opt => opt.WithApiKey("1234", "X-API-KEY"))
                ;

            var test = optionsBuilder as IApiOptionsBuilder;
            
            Assert.True(true);

            //var configRoot = BuildMockOptions();

            //var builder = new Mock<IConfigurationBuilder>();
            //builder.Setup(b => b.Build()).Returns(configRoot);

            //var options = BuildOptions(builder.Object);
            //builder.Object.AddRestApiConfigProvider(options);

            //var configSection = builder.Object.Build().GetSection("RestApi");
            //Assert.Multiple(() =>
            //{
            //    Assert.Equal(options.ClientId, configSection["ClientId"]);
            //    Assert.Equal(options.ClientSecret, configSection["ClientSecret"]);
            //    Assert.Equal(options.SiteUrl, configSection["SiteUrl"]);
            //    Assert.Equal(options.ProjectId, configSection["ProjectId"]);
            //    Assert.Equal(options.Environment, configSection["Environment"]);
            //});

        }

        //private static RestApiConfigProviderOptions BuildOptions(IConfigurationBuilder builder)
        //{
        //    var config = builder.Build().GetRequiredSection("RestApiConfigProvider");


        //    return new RestApiConfigProviderOptions
        //    {
        //        ClientId = config["ClientId"] ?? string.Empty,
        //        ClientSecret = config["ClientSecret"] ?? string.Empty,
        //        SiteUrl = config["SiteUrl"] ?? string.Empty,
        //        ProjectId = config["ProjectId"] ?? string.Empty,
        //        Environment = config["Environment"] ?? string.Empty,
        //    };
        //}

        //private static IConfigurationRoot BuildMockOptions()
        //{
        //    var dict = new Dictionary<string, string?>
        //    {
        //        { "RestApi:ClientId", Faker.Identification.UkNhsNumber() },
        //        { "RestApi:ClientSecret", Faker.Identification.UsPassportNumber() },
        //        { "RestApi:SiteUrl", Faker.Internet.Url() },
        //        { "RestApi:ProjectId", Faker.Internet.UserName() },
        //        { "RestApi:Environment", Faker.Lorem.GetFirstWord() },
        //    };

        //    return new ConfigurationBuilder()
        //        .AddInMemoryCollection(dict)
        //        .Build();
        //}
    }
}