using OLT.Extensions.Configuration.REST.Api.Builders;
using Flurl;
using Flurl.Http;

namespace OLT.Extensions.Configuration.REST.Api;

public interface IApiOptionsBuilder
{
    //ApiOptionsBuilder AddBuilder(IHttpClientBuilder builder);
    //ApiOptionsBuilder AddBuilder(IHttpClientQueryBuilder builder);
}

public class ApiOptionsBuilder : IApiOptionsBuilder
{
    private TimeSpan? _reloadAfter;
    private readonly string _endpoint;
    private readonly Flurl.Url _url;

    public ApiOptionsBuilder(string endpoint)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(endpoint);
        _endpoint = endpoint;
        _url = new Flurl.Url(endpoint);
        Request = new Flurl.Http.FlurlRequest(_url);
    }

    internal Flurl.Http.IFlurlRequest Request { get; }

    //private readonly List<IHttpClientBuilder> _httpClientBuilders = new List<IHttpClientBuilder>();

    //public ApiOptionsBuilder AddBuilder(IHttpClientBuilder builder)
    //{
    //    _httpClientBuilders.Add(builder);
    //    return this;
    //}

    //private readonly List<IHttpClientQueryBuilder> _httpQueryBuilders = new List<IHttpClientQueryBuilder>();
    //public ApiOptionsBuilder AddBuilder(IHttpClientQueryBuilder builder)
    //{
    //    _httpQueryBuilders.Add(builder);        
    //    return this;
    //}
    
}








//internal class AuthenticationBuilder : IAuthenticationBuilder
//{
//    public AuthenticationBuilder(IApiOptionsBuilder optionsBuilder)
//    {
//        OptionsBuilder = optionsBuilder;
//    }

//    public ApiOptionsBuilder OptionsBuilder { get; }

//}





public static class ApiConfigBuilderExtensions
{
    public static ApiOptionsBuilder UseAuthentication(this ApiOptionsBuilder builder, Action<AuthenticationBuilder> action)
    {
        action(new AuthenticationBuilder(builder));
        return builder;
    }

    /// <summary>
    /// Adds a parameter to the URL query, overwriting the value if name exists.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="queryKey"></param>
    /// <param name="queryValue"></param>
    /// <returns><see cref="ApiOptionsBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ApiOptionsBuilder SetQueryParam(this ApiOptionsBuilder builder, string queryKey, string queryValue)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNullOrEmpty(queryKey);
        ArgumentNullException.ThrowIfNullOrEmpty(queryValue);
        builder.Request.SetQueryParam(queryKey, queryValue, NullValueHandling.Remove);
        return builder;
    }

    /// <summary>
    ///  Sets an HTTP header est
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns><see cref="ApiOptionsBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ApiOptionsBuilder SetHeader(this ApiOptionsBuilder builder, string name, string value)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNullOrEmpty(value);
        builder.Request.WithHeader(name, value);
        return builder;
    }

    /// <summary>
    /// Add Http Header  "Accept" -> "application/json" 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns><see cref="ApiOptionsBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static ApiOptionsBuilder SetHeaderAcceptJson(this ApiOptionsBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Request.WithHeader("Accept", "application/json");
        return builder;
    }




  

}
