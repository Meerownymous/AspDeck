using Microsoft.AspNetCore.Builder;

namespace AspDeck;

public interface IBuild
{
    WebApplicationBuilder Implement(WebApplicationBuilder builder);
}