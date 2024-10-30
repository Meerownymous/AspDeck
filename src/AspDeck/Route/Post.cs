using AspDeck.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Tonga.Pipe;

namespace AspDeck.Route;

/// <summary>
/// Routing for http POST method and a pattern.
/// </summary>
public sealed class Post(
    string pattern,
    Func<HttpContext, Task<HttpResponse>> render,
    bool needsAuthentication = false
) :
    PipeEnvelope<IEndpointRouteBuilder>(
        new BaseRouting("POST", pattern, render, needsAuthentication)
    )
{
    /// <summary>
    /// Routing for http POST method and a pattern.
    /// </summary>
    public Post(
        string pattern,
        Func<HttpContext, HttpResponse> render,
        bool needsAuthentication = false) : this(
        pattern, context => Task.FromResult(render(context)), needsAuthentication
    )
    {
        
    }
}