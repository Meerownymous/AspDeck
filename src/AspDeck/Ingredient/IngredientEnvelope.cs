using Microsoft.AspNetCore.Builder;

namespace AspDeck.Ingredient;

/// <summary>
/// Envelope for Host Input bundles.
/// </summary>
public abstract class IngredientEnvelope(
    IIngredient origin
) : IIngredient
{
    public WebApplicationBuilder Implement(WebApplicationBuilder builder) =>
        origin.Implement(builder);

    public WebApplication Implement(WebApplication app) =>
        origin.Implement(app);
}