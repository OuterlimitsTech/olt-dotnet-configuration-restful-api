//using Flurl.Http;
//using System.Net.Http.Headers;
//using System.Xml.Linq;

//namespace OLT.Extensions.Configuration.REST.Api.Builders;

//internal class BearerTokenBuilder : IHttpHeaderBuilder
//{
 
//    public BearerTokenBuilder(string scheme, string token)
//    {
//        ArgumentNullException.ThrowIfNullOrEmpty(token);
//        Name = scheme ?? "Bearer";
//        Value = token;
//    }

//    public string Name { get; set; } 
//    public string Value { get; set; }

//    public void Build(IFlurlRequest request)
//    {
//        request.WithOAuthBearerToken(Value);
//        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Name, Value);
//    }
//}

