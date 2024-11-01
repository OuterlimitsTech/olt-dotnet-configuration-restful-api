﻿using OLT.Extensions.Configuration.RESTApi.Builders;
using Flurl;
using Flurl.Http;

namespace OLT.Extensions.Configuration.RESTApi;

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


    /// <summary>
    /// Adds Path Segment to the end of the endpoint
    /// </summary>
    /// <remarks>
    /// Base Endpoint = https://my.domain/api/configuration
    /// <list type="table">
    /// <item>
    ///   <term>app1</term>
    ///   <description>https://my.domain/api/configuration/app1</description>
    /// </item>
    /// <item>
    ///   <term>foobar</term>
    ///   <description>https://my.domain/api/configuration/foobar</description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <param name="builder"></param>
    /// <param name="segment"></param>
    /// <returns></returns>
    public static ApiOptionsBuilder AppendPathSegment(this ApiOptionsBuilder builder, string segment)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNullOrEmpty(segment);
        builder.Request.AppendPathSegment(segment);
        return builder;
    }


}
