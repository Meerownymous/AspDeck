using Microsoft.AspNetCore.Builder;
using Tonga.Pipe;

namespace AspDeck.Ingredient;

/// <summary>
/// Input to make a host.
/// </summary>
public sealed class AsIngredient(IPipe<WebApplicationBuilder> build, IPipe<WebApplication> configure) : IIngredient
{
    /// <summary>
    /// Input to make a host.
    /// </summary>
    public AsIngredient(
        Func<WebApplicationBuilder,WebApplicationBuilder> build,
        Func<WebApplication,WebApplication> configure
    ) : this(
        new AsPipe<WebApplicationBuilder>(build),
        new AsPipe<WebApplication>(configure)
    )
    { }
    
    /// <summary>
    /// Input to make a host.
    /// </summary>
    public AsIngredient(IPipe<WebApplicationBuilder> build) : this(
        build,
        new PassivePipe<WebApplication>()
    )
    { }
    
    /// <summary>
    /// Input to make a host.
    /// </summary>
    public AsIngredient(Func<WebApplicationBuilder,WebApplicationBuilder> build) : this(
        new AsPipe<WebApplicationBuilder>(build)
    )
    { }
    
    /// <summary>
    /// Input to make a host.
    /// </summary>
    public AsIngredient(IPipe<WebApplication> configure) : this(
        new PassivePipe<WebApplicationBuilder>(),
        configure
    )
    { }
    
    /// <summary>
    /// Input to make a host.
    /// </summary>
    public AsIngredient(Func<WebApplication,WebApplication> configure) : this(
        new PassivePipe<WebApplicationBuilder>(),
        new AsPipe<WebApplication>(configure)
    )
    { }
    
    public WebApplicationBuilder Implement(WebApplicationBuilder builder) => build.Push(builder);
    public WebApplication Implement(WebApplication app) => configure.Push(app);
}