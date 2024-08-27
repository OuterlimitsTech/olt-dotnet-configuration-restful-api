namespace OLT.Extensions.Configuration.RESTApi;

public interface IHttpHeaderBuilder : IHttpClientBuilder
{
    string Name { get; }
    string? Value { get; }
}
