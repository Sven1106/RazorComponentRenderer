# RazorComponentRenderer

A lightweight utility for rendering Razor components to HTML with a focus on simplicity and type safety.

## Features

- Render Razor components to HTML strings programmatically.
- Seamless ASP.NET Core DI via extension methods.
- Get strongly‑typed component parameters without extra setup:
    - `CreateComponentParameters(...)` methods are generated automatically for each Razor component.
    - Full IntelliSense support and compile‑time validation of names and types.
    - Zero runtime reflections or overhead (code is generated at build time).
- Support for EditForm, InputText, NavLink, AntiforgeryToken, and other Blazor
  components.
- Includes NavigationManager for URI/routing information during static rendering.

## Installation

```bash
dotnet add package RazorComponentRenderer
```

## Quick start

1. Register services:

```csharp
builder.Services.AddRazorComponentRenderer();
```

2. Create a component:

```razor
@* HelloWorld.razor *@

<h1>Hello @(Name ?? "World")!</h1>

@code {
    [Parameter] public string? Name { get; set; }
}
```

3. Render the component:

```csharp
app.MapGet("/hello", async (RazorComponentRendererService renderer, string? name) =>
{
    var html = await renderer.RenderComponent(HelloWorld.CreateComponentParameters(Name: name));

    return Results.Content(html, "text/html; charset=utf-8");
});
```

## Available Services

The `AddRazorComponentRenderer()` extension automatically registers:

- `RazorComponentRendererService` - Core rendering service
- `NavigationManager` - For URI/routing information
- `AntiforgeryStateProvider` - For CSRF protection
- `JSRuntime` - Provides clear error messages for JavaScript operations

## License

MIT
