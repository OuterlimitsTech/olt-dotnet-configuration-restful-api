namespace OLT.Extensions.Configuration.RESTApi.Builders;

public class AuthenticationBuilder
{
    internal AuthenticationBuilder(ApiOptionsBuilder optionsBuilder)
    {
        OptionsBuilder = optionsBuilder;
    }

    internal ApiOptionsBuilder OptionsBuilder { get; }
}

