# Simplify ASP WebApp creation
AspDeck provides a object oriented approach to structure and simplify the process of building an asp web application.

Opposed to the common building patterns of ASP, which - as the app grows - often result in a long and confusing setup file, you compose the app by reusable objects.

Example:
```csharp

await          
  new WebApp(                                  //the app container that wraps it up
    new Url("http://localhost:5001"),          //configures the given url
    new Routes(                                //wraps all the routes
      new Get("/readme", async context =>      //adds a get route
        await
          new HttpResponse(HttpStatusCode.OK)  //uses WHyLL, another object oriented messaging library with http support. But you can use whatever you want.
              .WithBody("Use AspDeck!")
              .To(new AspResponse(context))
      ),
      new Post("/stuff", async context =>      //adds a post route
        {
          // ... process stuff ...
          context.Response.StatusCode = 200;
          return context;
      )
  )
).StartAsync();                                //runs the webapp

```
