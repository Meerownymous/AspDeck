using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Tonga.Pipe;

namespace AspDeck.Routing;

/// <summary>
/// Routing base object, creating a route from method, pattern, and rendering method.
/// </summary>
public sealed class BaseRouting(
    string method,
    string pattern,
    Func<HttpContext, Task<HttpResponse>> render,
    bool needsAuthentication = false) :
    PipeEnvelope<IEndpointRouteBuilder>(
        new AsPipe<IEndpointRouteBuilder>(
            endpoints =>
            {
                
                Task<HttpResponse> Response(
                    HttpContext context, Func<HttpContext, Task<HttpResponse>> r, bool auth
                )
                {
                    Task<HttpResponse> result;
                    if (auth && context.User.Identity?.IsAuthenticated == true
                        || !auth
                       )
                        result = r(context);
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        result = Task.FromResult(context.Response);
                    }
                    return result;
                }
                
                if (method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                    endpoints.MapGet(pattern, 
                        async context => await Response(context, render, needsAuthentication));
                else if (method.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
                    endpoints.MapDelete(pattern, 
                        async context => await Response(context, render, needsAuthentication));
                else if (method.Equals("POST", StringComparison.OrdinalIgnoreCase))
                    endpoints.MapPost(pattern, 
                        async context => await Response(context, render, needsAuthentication));
                else if (method.Equals("PATCH", StringComparison.OrdinalIgnoreCase))
                    endpoints.MapPatch(pattern, 
                        async context => await Response(context, render, needsAuthentication));
                else if (method.Equals("PUT", StringComparison.OrdinalIgnoreCase))
                    endpoints.MapPut(pattern, 
                        async context => await Response(context, render, needsAuthentication));
                return endpoints;
            })
    )
{ }