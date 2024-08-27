using Microsoft.Extensions.Configuration;

namespace OLT.Extensions.Configuration.REST.Api;

public static class ConfigurationBuilderExtensions
{
    
    public static IConfigurationBuilder AddRestApiConfigProvider(this IConfigurationBuilder builder, string endpoint, bool optional, Action<ApiOptionsBuilder> action)
    {
        var optionsBuilder = new ApiOptionsBuilder(endpoint);
        action(optionsBuilder);


        return builder.AddRestApiConfigProvider(ConfigureSource(new RestApiConfigProviderOptions(), optional, null));
    }




    /// <summary>
    /// Adds an <see cref="IConfigurationProvider"/> that reads configuration values from RestApiConfigProvider
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configureSource"></param>
    /// <returns>The <see cref="IConfigurationBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IConfigurationBuilder AddRestApiConfigProvider(this IConfigurationBuilder builder, Action<RestApiConfigProviderConfigurationSource> configureSource)
    {
        if (configureSource == null) throw new ArgumentNullException(nameof(configureSource));
        var source = new RestApiConfigProviderConfigurationSource();
        configureSource(source);
        return builder.Add(source);
    }


    private static Action<RestApiConfigProviderConfigurationSource> ConfigureSource(RestApiConfigProviderOptions RestApiConfigProviderOptions, bool optional = false, TimeSpan? reloadAfter = null)
    {
        return configurationSource =>
        {
            configurationSource.RestApiConfigProviderOptions = RestApiConfigProviderOptions ?? new RestApiConfigProviderOptions();
            configurationSource.Optional = optional;
            configurationSource.ReloadAfter = reloadAfter;
        };
    }





}
