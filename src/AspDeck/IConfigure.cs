using Microsoft.AspNetCore.Builder;

namespace AspDeck;

/// <summary>
/// Configure a WebApp after building it.
/// </summary>
public interface IConfigure
{
    WebApplication Implement(WebApplication app);
}