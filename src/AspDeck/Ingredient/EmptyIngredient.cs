using AspDeck.Host;
using Microsoft.AspNetCore.Builder;
using Tonga.Pipe;

namespace AspDeck.Ingredient;

/// <summary>
/// Host input which is empty.
/// </summary>
public sealed class EmptyIngredient() : IngredientEnvelope(
    new AsIngredient(
        new PassivePipe<WebApplicationBuilder>(),
        new PassivePipe<WebApplication>()
    )    
)
{ }