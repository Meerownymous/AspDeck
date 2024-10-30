using Tonga.Enumerable;

namespace AspDeck.Ingredient;

/// <summary>
/// Multiple <see cref="IIngredient"/> as one.
/// </summary>
/// <param name="inputs"></param>
public sealed class JoinedIngredient(IEnumerable<IIngredient> inputs) : IngredientEnvelope(
    new AsIngredient(
        builder =>
        {
            foreach (var input in inputs)
                builder = input.Implement(builder);
            return builder;
        },
        app =>
        {
            foreach (var input in inputs)
                app = input.Implement(app);
            return app;
        }
    )
)
{
    public JoinedIngredient(params IIngredient[] inputs) : this(AsEnumerable._(inputs))
    { }
}