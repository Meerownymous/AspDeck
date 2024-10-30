using AspDeck.Host;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspDeck.Ingredient;

/// <summary>
/// Periodically accomplished work.
/// </summary>
public sealed class Cycle(string name, Func<Task> work, double intervalSeconds, IOptional<ILogger<Cycle>> log) :
    IngredientEnvelope(
        new AsIngredient(
            builder =>
            {
                var cycle = new CycleService(name, work, intervalSeconds, log);
                builder.Services.AddHostedService(_ => cycle);
                return (WebApplicationBuilder)builder;
            }
        )
    )
{
    /// <summary>
    /// Periodically accomplished work.
    /// </summary>
    public Cycle(
        string name, 
        Func<Task> work, 
        int intervalSeconds
    ) : this(
        name, work, intervalSeconds, new OptEmpty<ILogger<Cycle>>()
    )
    { }
    
    /// <summary>
    /// Periodically accomplished work.
    /// </summary>
    public Cycle(
        string name, 
        Func<Task> work, 
        double intervalSeconds,
        ILogger<Cycle> log) : this(
        name, work, intervalSeconds, new OptFull<ILogger<Cycle>>(log)
    )
    { }
    
    /// <summary>
    /// Background service implementation.
    /// </summary>
    internal sealed class CycleService : BackgroundService
    {
        private readonly string name;
        private readonly Func<Task> work;
        private readonly IOptional<ILogger<Cycle>> log;
        private readonly TimeSpan interval;

        internal CycleService(string name, Func<Task> work, double intervalSeconds, IOptional<ILogger<Cycle>> log)
        {
            this.name = name;
            this.work = work;
            this.log = log;
            this.interval = TimeSpan.FromSeconds(intervalSeconds);
        }

        // This method will be called periodically
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                log.IfHas(l => l.LogInformation($"{name} running at: {DateTimeOffset.Now}"));
                try
                {
                    await work();
                }catch(Exception ex)
                {
                    log.IfHas(l => l.LogError($"Error executing {this.name}: {ex.Message}\r\n{ex.StackTrace}"));
                }
                await Task.Delay(interval, stoppingToken);
            }
        }
    }
}