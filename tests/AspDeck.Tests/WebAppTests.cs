using System.Net;
using AspDeck.Route;
using Tonga.IO;
using WHyLL.AspNet.Warp;
using WHyLL.Http.Warp;
using WHyLL.Warp;
using Xunit;
using HttpResponse = WHyLL.Http.Response.HttpResponse;
using Url = AspDeck.Ingredient.Url;

namespace AspDeck.Tests;

public sealed class WebAppTests
{
    [Fact]
    public async Task Builds()
    {
        using var app =
            new WebApp(
                new Routes(
                    new Get("/unittest", async context =>
                        await
                            new HttpResponse(HttpStatusCode.OK)
                                .WithBody(new AsInputStream("success"))
                                .To(new AspResponse(context))
                    )
                ),
                new Url("http://localhost:5001")
            );
        try
        {
            await app.StartAsync();
            await Task.Delay(1000);

            Assert.Equal(
                "success",
                await new WHyLL.Http.Request.Get("http://localhost:5001/unittest")
                    .To(new HttpWire(new HttpClient()))
                    .To(new BodyAsText())
            );
        }
        finally
        {
            await app.StopAsync();    
        }
    }   
}