using Microsoft.Extensions.Configuration;

namespace OLT.Extensions.Configuration.RESTApi;

public static class ConfigurationBuilderExtensions
{
    /// <summary>
    /// Configure a RESTful API configuration source
    /// </summary>
    /// <remarks>
    /// NOTE: This configuration builds a default Http request without Authenication, Headers, or Query Parameters
    /// </remarks>
    /// <param name="builder"></param>
    /// <param name="endpoint"></param>
    /// <param name="optional"></param>
    /// <returns>The <see cref="IConfigurationBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IConfigurationBuilder AddRestApi(this IConfigurationBuilder builder, string endpoint, bool optional)
    {
        return builder.AddRestApi(ConfigureSource(new ApiOptionsBuilder(endpoint), optional, null));
    }

    /// <summary>
    /// Configure a RESTful API configuration source that reloads using the <paramref name="reloadAfter"/> interval
    /// </summary>
    /// <remarks>
    /// NOTE: This configuration builds a default Http request without Authenication, Headers, or Query Parameters
    /// </remarks>
    /// <param name="builder"></param>
    /// <param name="endpoint"></param>
    /// <param name="optional"></param>
    /// <param name="reloadAfter"></param>
    /// <returns>The <see cref="IConfigurationBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IConfigurationBuilder AddRestApi(this IConfigurationBuilder builder, string endpoint, bool optional, TimeSpan reloadAfter)
    {
        return builder.AddRestApi(ConfigureSource(new ApiOptionsBuilder(endpoint), optional, reloadAfter));
    }

    /// <summary>
    /// Configure a RESTful API configuration source
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="endpoint"></param>
    /// <param name="optional"></param>
    /// <param name="action"></param>
    /// <returns>The <see cref="IConfigurationBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IConfigurationBuilder AddRestApi(this IConfigurationBuilder builder, string endpoint, bool optional, Action<ApiOptionsBuilder> action)
    {
        var optionsBuilder = new ApiOptionsBuilder(endpoint);
        action(optionsBuilder);
        return builder.AddRestApi(ConfigureSource(optionsBuilder, optional, null));
    }

    /// <summary>
    /// Configure a RESTful API configuration source that reloads using the <paramref name="reloadAfter"/> interval
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="endpoint"></param>
    /// <param name="optional"></param>
    /// <param name="reloadAfter"></param>
    /// <param name="action"></param>
    /// <returns>The <see cref="IConfigurationBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IConfigurationBuilder AddRestApi(this IConfigurationBuilder builder, string endpoint, bool optional, TimeSpan reloadAfter, Action<ApiOptionsBuilder> action)
    {
        var optionsBuilder = new ApiOptionsBuilder(endpoint);
        action(optionsBuilder);
        return builder.AddRestApi(ConfigureSource(optionsBuilder, optional, reloadAfter));
    }



    /// <summary>
    /// Adds an <see cref="IConfigurationProvider"/> that reads configuration values from RestApiConfigProvider
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configureSource"></param>
    /// <returns>The <see cref="IConfigurationBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IConfigurationBuilder AddRestApi(this IConfigurationBuilder builder, Action<RestApiProviderConfigurationSource> configureSource)
    {
        if (configureSource == null) throw new ArgumentNullException(nameof(configureSource));
        var source = new RestApiProviderConfigurationSource();        
        configureSource(source);
        return builder.Add(source);
    }

    private static Action<RestApiProviderConfigurationSource> ConfigureSource(ApiOptionsBuilder optionsBuilder, bool optional = false, TimeSpan? reloadAfter = null)
    {
        return configurationSource =>
        {
            configurationSource.Request = optionsBuilder.Request ?? new Flurl.Http.FlurlRequest();
            configurationSource.Optional = optional;
            configurationSource.ReloadAfter = reloadAfter;
        };
    }

}
