using AspDeck.Ingredient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Tonga.Pipe;

namespace AspDeck.Route;

/// <summary>
/// An app's route configuration.
/// </summary>
public sealed class Routes(params IPipe<IEndpointRouteBuilder>[] configure) :
    IngredientEnvelope(
        new AsIngredient(
            app =>
            {
                app.UseRouting()
                    .UseEndpoints(endpoints =>
                        new JoinedPipe<IEndpointRouteBuilder>(configure)
                            .Push(endpoints)
                    );
                return app;
            }
        )
    );