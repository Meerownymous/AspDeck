using Microsoft.AspNetCore.Builder;
using Tonga.Pipe;

namespace AspDeck.Host;

/// <summary>
/// Build only if condition is true.
/// </summary>
public sealed class BuildIf(
    Func<WebApplicationBuilder,bool> condition, 
    IPipe<WebApplicationBuilder> consequence
) : PipeEnvelope<WebApplicationBuilder>(
    new IfPipe<WebApplicationBuilder>(condition, consequence)
)
{ }