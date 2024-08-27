//namespace OLT.Extensions.Configuration.REST.Api.Builders;




//internal class ApiKeyHeaderBuilder : IHttpClientBuilder
//{
//    public ApiKeyHeaderBuilder()
//    {

//    }

//    public ApiKeyHeaderBuilder(string name, string? value)
//    {
//        ArgumentNullException.ThrowIfNullOrEmpty(name);
//        ArgumentNullException.ThrowIfNullOrEmpty(value);

//        Name = name;
//        Value = value;
//    }

//    public string Name { get; set; } = "X-API-KEY";
//    public string? Value { get; set; }

//    public void Build(HttpClient client)
//    {
//        client.DefaultRequestHeaders.Add(Name, Value);
//    }

//}

