using Microsoft.Extensions.Configuration;

namespace OLT.Extensions.Configuration.RESTApi;


public class RestApiProviderConfigurationSource : IConfigurationSource
{
    public bool Optional { get; set; }
    public TimeSpan? ReloadAfter { get; set; }
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);
    public Flurl.Http.IFlurlRequest Request { get; set; } = new Flurl.Http.FlurlRequest();

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new RestApiConfigProviderConfigurationProvider(this);
    }

}