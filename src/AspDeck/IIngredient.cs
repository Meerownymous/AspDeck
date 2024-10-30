

using Microsoft.AspNetCore.Builder;

namespace AspDeck;

/// <summary>
/// Input for building and configuring a host.
/// </summary>
public interface IIngredient
{
    WebApplicationBuilder Implement(WebApplicationBuilder builder);
    WebApplication Implement(WebApplication app);
}