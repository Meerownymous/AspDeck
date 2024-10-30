using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using AspDeck.Ingredient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspDeck.Host;

/// <summary>
/// Basic authorization.
/// </summary>
public sealed class BasicAuth : IngredientEnvelope
{
    /// <summary>
    /// Basic authorization.
    /// </summary>
    public BasicAuth(IConfiguration config) : base(
        new AsIngredient(
            builder =>
            {
                builder.Services.AddSingleton(config);
                builder.Services.AddAuthorization();
                return builder;
            },
            app =>
            {
                app.UseMiddleware<BasicAuthenticationMiddleware>();
                app.UseAuthorization();
                return app;
            }
        )
    )
    { }

    public sealed class BasicAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        private readonly Lazy<string> clientId = new(() => configuration["Authentication:ClientId"]);
        private readonly Lazy<string> clientSecret = new(() => configuration["Authentication:ClientSecret"]);

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                await next(context);
                return;
            }

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');

                // Validate the clientId and clientSecret
                if (clientId.Value == credentials[0] && clientSecret.Value == credentials[1])
                {
                    var claims = new[] {
                        new Claim(ClaimTypes.NameIdentifier, credentials[0]),
                        new Claim(ClaimTypes.Name, credentials[1]),
                    };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
            catch
            {
                // Do nothing if the header is invalid
            }

            await next(context);
        }
    }
}