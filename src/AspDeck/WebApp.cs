using AspDeck.Host;
using AspDeck.Ingredient;
using Microsoft.AspNetCore.Builder;

namespace AspDeck;

/// <summary>
/// Host assembled from <see cref="IIngredient"/>
/// </summary>
public sealed class WebApp : HostEnvelope
{
    public WebApp(params IIngredient[] input) : base(
        new AsHost(() =>
        {
            var joined = new JoinedIngredient(input);
            return joined.Implement(joined.Implement(WebApplication.CreateBuilder([])).Build());
        })
    )
    {
        
    }

    /// <summary>
    /// Host assembled from pipes.
    /// </summary>
    public WebApp(
        IPipe<WebApplicationBuilder> build,
        IPipe<WebApplication> configure
    ) : this(new AsIngredient(build, configure))
    { }
}