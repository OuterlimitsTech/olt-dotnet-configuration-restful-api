using Microsoft.Extensions.Configuration;

namespace OLT.Extensions.Configuration.REST.Api;


public class RestApiConfigProviderConfigurationSource : IConfigurationSource
{
    public bool Optional { get; set; }
    public TimeSpan? ReloadAfter { get; set; }
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);
    public RestApiConfigProviderOptions RestApiConfigProviderOptions { get; set; } = new RestApiConfigProviderOptions();

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new RestApiConfigProviderConfigurationProvider(this);
    }

}