namespace OLT.Extensions.Configuration.REST.Api;

public interface IHttpQueryBuilder : IHttpClientBuilder
{
    string Key { get; }
    string Value { get; }
}