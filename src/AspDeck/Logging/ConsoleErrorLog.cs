using AspDeck.Ingredient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AspDeck.Host;

/// <summary>
/// Adds Console error logging.
/// </summary>
public sealed class ConsoleErrorLog() :
    IngredientEnvelope(
        new AsIngredient(
            config =>
            {
                config.UseMiddleware<ExceptionLogging>();
                return config;
            }
        )
    )
{
    public class ExceptionLogging(RequestDelegate next, ILogger<ExceptionLogging> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context); // Call the next middleware in the pipeline
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception has occurred."); // Log the exception
                throw;
            }
        }
    }
}