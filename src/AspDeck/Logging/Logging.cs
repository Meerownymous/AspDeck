using AspDeck.Ingredient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

/// <summary>
/// Adds logging to the webapp.
/// </summary>
public sealed class Logging : IngredientEnvelope
{
    /// <summary>
    /// Adds logging to the webapp.
    /// </summary>
    public Logging(ILogger logger) : this(pipeWebAppBuilder: builder =>
    {
        builder.Services.AddSingleton(logger);
        return builder;
    })
    { }

    /// <summary>
    /// Adds logging to the webapp.
    /// </summary>
    public Logging(Func<ILoggingBuilder, ILoggingBuilder> pipeLoggingBuilder) : base(
        new AsIngredient(builder =>
        {
            _ = pipeLoggingBuilder(builder.Logging);
            return builder;
        })
    )
    { }
    
    private Logging(Func<WebApplicationBuilder, WebApplicationBuilder> pipeWebAppBuilder) : base(
        new AsIngredient(pipeWebAppBuilder)    
    )
    { }
}