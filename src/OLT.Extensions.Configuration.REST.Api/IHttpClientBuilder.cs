namespace OLT.Extensions.Configuration.REST.Api;

public interface IHttpClientBuilder
{
    void Build(Flurl.Http.IFlurlRequest request);
}
