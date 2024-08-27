namespace OLT.Extensions.Configuration.REST.Api;

public interface IHttpHeaderBuilder : IHttpClientBuilder
{
    string Name { get; }
    string? Value { get; }
}
