using Flurl.Http;
using OLT.Extensions.Configuration.RESTApi.Builders;

namespace OLT.Extensions.Configuration.RESTApi;

public static class AuthenticationBuilderExtensions
{
    public static void WithApiKey(this AuthenticationBuilder builder, string apiKey, string headerName = "X-API-KEY")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNullOrEmpty(apiKey);
        ArgumentNullException.ThrowIfNullOrEmpty(headerName);
        builder.OptionsBuilder.Request.WithHeader(headerName, apiKey);
    }

    public static void WithBearerToken(this AuthenticationBuilder builder, string token)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNullOrEmpty(token);
        //builder.OptionsBuilder.AddBuilder(new BearerTokenBuilder(headerKey, token));
        builder.OptionsBuilder.Request.WithOAuthBearerToken(token);
    }

    public static void WithBasicAuth(this AuthenticationBuilder builder, string username, string password)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNullOrEmpty(username);
        ArgumentNullException.ThrowIfNullOrEmpty(password);
        //builder.OptionsBuilder.AddBuilder(new BearerTokenBuilder(headerKey, token));
        builder.OptionsBuilder.Request.WithBasicAuth(username, password);
    }
}