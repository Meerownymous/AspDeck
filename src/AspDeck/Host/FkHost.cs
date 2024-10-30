using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspDeck.Host;

/// <summary>
/// Fake <see cref="IHost"/>
/// </summary>
public sealed class FkHost : IHost
{
    public void Dispose() { }

    public Task StartAsync(CancellationToken cancellationToken = new()) =>
        Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken = new()) =>
        Task.CompletedTask;

    public IServiceProvider Services
    {
        get => new DefaultServiceProviderFactory()
            .CreateServiceProvider(new ServiceCollection());
    }
}