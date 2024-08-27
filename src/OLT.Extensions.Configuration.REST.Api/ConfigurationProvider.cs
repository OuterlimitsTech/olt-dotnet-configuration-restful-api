using System.Collections.Frozen;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace OLT.Extensions.Configuration.REST.Api;


public class RestApiConfigProviderConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider, IDisposable
{
    //private readonly Lazy<RestApiConfigProviderClient> _RestApiConfigProviderClient;
    private readonly RestApiConfigProviderConfigurationSource _source;
    private readonly Timer? _refreshTimer;


    private static readonly TimeSpan MinDelayForUnhandledFailure = TimeSpan.FromSeconds(5);
    private bool _isInitialLoadComplete;
    private int _networkOperationsInProgress;


    public RestApiConfigProviderConfigurationProvider(RestApiConfigProviderConfigurationSource source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //ArgumentException.ThrowIfNullOrEmpty(source.RestApiConfigProviderOptions.SiteUrl, "RestApiConfigProviderOptions.SiteUrl");
        //ArgumentException.ThrowIfNullOrEmpty(source.RestApiConfigProviderOptions.ClientId, "RestApiConfigProviderOptions.ClientId");
        //ArgumentException.ThrowIfNullOrEmpty(source.RestApiConfigProviderOptions.ClientSecret, "RestApiConfigProviderOptions.ClientSecret");
        //ArgumentException.ThrowIfNullOrEmpty(source.RestApiConfigProviderOptions.Environment, "RestApiConfigProviderOptions.Environment");
        //ArgumentException.ThrowIfNullOrEmpty(source.RestApiConfigProviderOptions.ProjectId, "RestApiConfigProviderOptions.ProjectId");

        //_RestApiConfigProviderClient = new Lazy<RestApiConfigProviderClient>(() =>
        //{
        //    ClientSettings settings = new ClientSettings
        //    {
        //        SiteUrl = source.RestApiConfigProviderOptions.SiteUrl,
        //        Auth = new AuthenticationOptions
        //        {
        //            UniversalAuth = new UniversalAuthMethod
        //            {
        //                ClientId = source.RestApiConfigProviderOptions.ClientId,
        //                ClientSecret = source.RestApiConfigProviderOptions.ClientSecret
        //            }
        //        }
        //    };

        //    return new RestApiConfigProviderClient(settings);
        //});

        if (source.ReloadAfter != null)
        {
            _refreshTimer = new Timer(OnTimerElapsed, null, TimeSpan.Zero, source.ReloadAfter.Value);
        }

    }


    public override void Load()
    {

        var stopwatch = Stopwatch.StartNew();
        try
        {
            Load(false);
        }
        catch
        {
            var delay = MinDelayForUnhandledFailure.Subtract(stopwatch.Elapsed);
            if (delay.Ticks > 0L)
                Task.Delay(delay).ConfigureAwait(false).GetAwaiter().GetResult();

            if (!_source.Optional)
                throw;
        }
        finally
        {
            _isInitialLoadComplete = true;
        }
    }

    private void Load(bool reload)
    {
    //    Load(secrets =>
    //    {
    //        var data = new Dictionary<string, string>();
    //        foreach (var (key, value) in secrets)
    //        {
    //            data.Add(key, value.SecretValue);
    //        }
    //        Data = data!;

    //        if (reload)
    //        {
    //            OnReload();
    //        }
    //    });
    }


    //private void Load(Action<IDictionary<string, SecretElement>> callback)
    //{
    //    var task = Task.Run(() => callback(LoadSecrets()));
    //    if (!task.Wait(_source.Timeout))
    //        throw new Exception("Timeout while loading secrets.");
    //}

    //private FrozenDictionary<string, SecretElement> LoadSecrets()
    //{
    //    var request = new ListSecretsOptions
    //    {
    //        Environment = _source.RestApiConfigProviderOptions.Environment,
    //        ProjectId = _source.RestApiConfigProviderOptions.ProjectId,
    //        Path = _source.RestApiConfigProviderOptions.Path,
    //        Recursive = _source.RestApiConfigProviderOptions.Recursive
    //    };

    //    return _RestApiConfigProviderClient.Value.ListSecrets(request).ToFrozenDictionary(s => s.SecretKey);
    //}

    private void OnTimerElapsed(object? state)
    {
        if (!_isInitialLoadComplete)
        {
            return;
        }

        if (Interlocked.Exchange(ref _networkOperationsInProgress, 1) != 0) return;

        try
        {
            Load(true);
        }
        catch
        {
            if (!_source.Optional)
            {
                throw;
            }
        }
        finally
        {
            Interlocked.Exchange(ref _networkOperationsInProgress, 0);
        }


    }


    public void Dispose()
    {
        _refreshTimer?.Dispose();

        //if (!_RestApiConfigProviderClient.IsValueCreated)
        //    return;

        //_RestApiConfigProviderClient.Value.Dispose();
    }

}
