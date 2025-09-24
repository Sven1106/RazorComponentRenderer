# RazorComponentRenderer

A lightweight utility for rendering Razor components to HTML with a focus on simplicity and type safety.

## Features

- Render Razor components to HTML strings programmatically.
- Integrate easily with ASP.NET Core DI via extension methods.
- Get strongly‑typed component parameters without extra setup:
    - `CreateComponentParameters(...)` methods are generated automatically for each Razor component.
    - Full IntelliSense support and compile‑time validation of names and types.
    - Zero runtime reflection or overhead (code is generated at build time).

## Installation

Add the library to your .NET solution as a project reference or via NuGet (when available).

Example: build and pack locally (adjust to your environment)

```
cd ./RazorComponentRenderer
./build-package.ps1
```

## Quick start

1) Register services:

```csharp
builder.Services.AddRazorComponentRenderer();
``` 

2) Create a component:

```razor
@* HelloWorld.razor *@

<h1>Hello @(Name ?? "World")!</h1>

@code {
    [Parameter] public string? Name { get; set; }
}
```

3) Render the component:

```csharp
app.MapGet("/hello", async (RazorComponentRendererService renderer, string? name) =>
{
    var html = await renderer.RenderComponent(HelloWorld.CreateComponentParameters(name: name));

    return Results.Content(html, "text/html");
});
```

## Contributing

Issues and PRs are welcome. Please include a clear description and, when applicable, tests or reproduction steps.

## License

MIT (or the license specified in the repository).
