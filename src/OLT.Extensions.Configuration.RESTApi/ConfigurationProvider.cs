using Flurl.Http;
using System.Collections.Frozen;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace OLT.Extensions.Configuration.RESTApi;


public class RestApiConfigProviderConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider, IDisposable
{
    private readonly Lazy<IFlurlClient> _client;
    private readonly RestApiProviderConfigurationSource _source;
    private readonly Timer? _refreshTimer;

    private static readonly TimeSpan MinDelayForUnhandledFailure = TimeSpan.FromSeconds(5);
    private bool _isInitialLoadComplete;
    private int _networkOperationsInProgress;


    public RestApiConfigProviderConfigurationProvider(RestApiProviderConfigurationSource source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));

        _source.Request.WithTimeout(_source.Timeout);

        _client = new Lazy<IFlurlClient>(() =>
        {
            var result = _source.Request.EnsureClient();            
            return result;
        });

        if (source.ReloadAfter != null)
        {
            _refreshTimer = new Timer(OnTimerElapsed, null, TimeSpan.Zero, source.ReloadAfter.Value);
        }

    }


    public override void Load()
    {        
        try
        {
            LoadAsync(false).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch
        {
            throw;
        }
        finally
        {
            _isInitialLoadComplete = true;
        }
    }

    private async Task LoadAsync(bool reload)
    {
     
        try
        {
            var newData = await _source.Request.GetJsonAsync<Dictionary<string, string?>>().ConfigureAwait(false) ?? new Dictionary<string, string?>();
            if (Data != null && !Data.EquivalentTo(newData))
            {
                Data = newData;

                if (reload)
                {
                    OnReload();
                }                
            }

        }
        catch
        {
            if (_source.Optional) return;

            if (!reload) throw;
        }

        
    }

    private void OnTimerElapsed(object? state)
    {
        if (!_isInitialLoadComplete)
        {
            return;
        }

        if (Interlocked.Exchange(ref _networkOperationsInProgress, 1) != 0) return;

        try
        {
            LoadAsync(true).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch
        {
            throw;
        }
        finally
        {
            Interlocked.Exchange(ref _networkOperationsInProgress, 0);
        }


    }


    public void Dispose()
    {
        _refreshTimer?.Dispose();

        if (!_client.IsValueCreated)
            return;

        _client.Value.Dispose();        
    }

}
