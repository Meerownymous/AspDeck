using Microsoft.Extensions.Hosting;

namespace AspDeck.Host;

/// <summary>
/// Host from lambda.
/// </summary>
public sealed class AsHost : IHost
{
    private readonly Lazy<IHost> host;

    /// <summary>
    /// Host form lambda function.
    /// </summary>
    public AsHost(Func<IHost> host) =>
        this.host = new Lazy<IHost>(host.Invoke);

    public IServiceProvider Services => this.host.Value.Services;
    public void Dispose() => this.host.Value.Dispose();

    public Task StartAsync(CancellationToken cancellationToken = default) =>
        this.host.Value.StartAsync(cancellationToken);

    public Task StopAsync(CancellationToken cancellationToken = default) =>
        this.host.Value.StopAsync(cancellationToken);
}

