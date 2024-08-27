namespace OLT.Extensions.Configuration.REST.Api.Builders;

public interface IAuthenticationBuilder
{
    
}

public class AuthenticationBuilder : IAuthenticationBuilder
{
    internal AuthenticationBuilder(ApiOptionsBuilder optionsBuilder)
    {
        OptionsBuilder = optionsBuilder;
    }

    internal ApiOptionsBuilder OptionsBuilder { get; }
}

public interface IHeaderBuilder
{

}

public class HeaderBuilder : IHeaderBuilder
{
    internal HeaderBuilder(ApiOptionsBuilder optionsBuilder)
    {
        OptionsBuilder = optionsBuilder;
    }

    internal ApiOptionsBuilder OptionsBuilder { get; }
}

