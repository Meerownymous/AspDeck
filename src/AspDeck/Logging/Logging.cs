using AspDeck.Ingredient;
using Microsoft.Extensions.Logging;

namespace AspDeck.Host;

/// <summary>
/// WebApplication Logger Building Pipe.
/// </summary>
public sealed class Logging(Func<ILoggingBuilder,ILoggingBuilder> pipe) : 
    IngredientEnvelope(
        new AsIngredient(
            builder =>
            {
                pipe(builder.Logging);
                return builder;
            }
        )
)
{ }

