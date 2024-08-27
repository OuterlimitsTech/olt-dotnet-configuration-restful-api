using Flurl.Http;

namespace OLT.Extensions.Configuration.RESTApi.Builders;

internal class QueryBuilder : IHttpQueryBuilder
{
    public QueryBuilder(string key, string value)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(key);
        ArgumentNullException.ThrowIfNullOrEmpty(value);
        Key = key;
        Value = value;
    }

    public string Key { get; set; }
    public string Value { get; set; }

    public void Build(IFlurlRequest request)
    {        
        request.SetQueryParam(Key, Value, Flurl.NullValueHandling.Remove);
    }
}

