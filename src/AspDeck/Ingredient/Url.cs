namespace AspDeck.Ingredient;

/// <summary>
/// Add a specific url to the app.
/// </summary>
public sealed class Url: IngredientEnvelope
{
    public Url(Uri url) : base(
        new AsIngredient(app =>
        {
            app.Urls.Add(url.ToString());
            return app;
        })
    )
    { }

    /// <summary>
    /// Add a specific url to the app.
    /// </summary>
    public Url(string url) : this(new Uri(url))
    { }
}