# .NET Configuration Extensions for using a RESTful endpoint


[![Nuget](https://img.shields.io/nuget/v/OLT.Extensions.Configuration.RESTApi)](https://www.nuget.org/packages/OLT.Extensions.Configuration.RESTApi)


[![CI](https://github.com/OuterlimitsTech/olt-dotnet-configuration-restful-api/actions/workflows/build.yml/badge.svg)](https://github.com/OuterlimitsTech/olt-dotnet-configuration-restful-api/actions/workflows/build.yml) 


```shell
dotnet add package OLT.Extensions.Configuration.RESTApi
```

### Simple

```csharp
using OLT.Extensions.Configuration.RESTApi;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddRestApiConfigProvider("https://my-configuration-endpoint/config/application", false, TimeSpan.FromMinutes(10),  options =>
            {
                options
                    .SetHeaderAcceptJson()
                    .SetQueryParam("env", "staging")
                    .UseAuthentication(auth => auth.WithApiKey(apiKey, "X-API-KEY"));

            });


```

