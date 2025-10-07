using Example.Components;
using RazorComponentRenderer;
using RazorComponentRenderer.DependencyInjection.ServiceCollectionExtensionMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponentRenderer();

var app = builder.Build();
app.MapGet("/", () => Results.Redirect("/hello"));
app.MapGet("/hello", async (RazorComponentRendererService renderer, string? name) =>
{
    var html = await renderer.RenderComponent(HelloWorld.CreateComponentParameters(Name: name));
    
    return Results.Content(html, "text/html; charset=utf-8");
});

app.Run();