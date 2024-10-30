using Microsoft.Extensions.Hosting;

namespace AspDeck.Host
{
    /// <summary>
    /// Envelope for Asp Web Hosts.
    /// </summary>
    public abstract class HostEnvelope : IHost
    {
        private readonly IHost host;

        /// <summary>
        /// Envelope for Asp Web Hosts.
        /// </summary>
        public HostEnvelope(IHost host)
        {
            this.host = host;
        }

        public IServiceProvider Services => this.host.Services;
        public void Dispose() => this.host.Dispose();
        public Task StartAsync(CancellationToken cancellationToken = default) =>
            this.host.StartAsync(cancellationToken);
        public Task StopAsync(CancellationToken cancellationToken = default) =>
            this.host.StopAsync(cancellationToken);
    }
}

