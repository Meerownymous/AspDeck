// Decompiled with JetBrains decompiler
// Type: AspDeck.WebApp
// Assembly: AspDeck, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71D0B19F-B35D-4493-8CEC-6BC6E969DBE5
// Assembly location: /Users/meerow/.nuget/packages/aspdeck/0.5.0/lib/net9.0/AspDeck.dll

using AspDeck.Host;
using AspDeck.Ingredient;
using Microsoft.AspNetCore.Builder;

namespace AspDeck;

/// <summary>
/// Webapp made from builder and ingredients.
/// </summary>
public sealed class WebApp(Func<WebApplicationBuilder> makeBuilder, params IIngredient[] input) : HostEnvelope(
    new AsHost(() =>
    {
        JoinedIngredient joinedIngredient = new JoinedIngredient(input);
        return joinedIngredient.Implement(
            joinedIngredient.Implement(makeBuilder()).Build()
        );
    }))
{
    /// <summary>
    /// Webapp made from ingredients.
    /// </summary>
    public WebApp(
        params IIngredient[] input
    ) : this(() => WebApplication.CreateBuilder([]), input)
    { }
    
    /// <summary>
    /// Webapp made from builder and ingredients.
    /// </summary>
    public WebApp(
        WebApplicationBuilder builder,
        params IIngredient[] input
    ) : this(() => builder, input)
    { }
        
    /// <summary>
    /// Webapp made from builder and pipes.
    /// </summary>
    public WebApp(
        Func<WebApplicationBuilder> makeBuilder,
        IPipe<WebApplicationBuilder> build, 
        IPipe<WebApplication> configure
    ) : this(
        makeBuilder,
        new AsIngredient(build, configure)
    )
    { }
        
    /// <summary>
    /// Webapp made from builder and pipes.
    /// </summary>
    public WebApp(
        WebApplicationBuilder builder,
        IPipe<WebApplicationBuilder> build, 
        IPipe<WebApplication> configure
    ) : this(
        () => builder,
        new AsIngredient(build, configure)
    )
    { }
    
    /// <summary>
    /// Webapp made from pipes.
    /// </summary>
    public WebApp(
        IPipe<WebApplicationBuilder> build, 
        IPipe<WebApplication> configure
    ) : this(
        () => WebApplication.CreateBuilder([]),
        build,
        configure
    )
    { }
}