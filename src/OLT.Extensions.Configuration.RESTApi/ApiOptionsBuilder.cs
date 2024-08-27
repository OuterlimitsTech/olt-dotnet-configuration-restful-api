namespace OLT.Extensions.Configuration.RESTApi;


public class ApiOptionsBuilder
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


}
