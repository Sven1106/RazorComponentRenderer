using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;

namespace RazorComponentRenderer.StaticRendering;

/// <summary>
/// A minimal <see cref="Microsoft.AspNetCore.Components.NavigationManager"/> implementation used during static HTML rendering
/// when components are rendered through the <see cref="RazorComponentRendererService.RenderComponent"/> method.
/// <para>
/// The regular <see cref="Microsoft.AspNetCore.Components.NavigationManager"/> requires a Blazor runtime environment and cannot
/// be used when rendering components outside an interactive circuit. This implementation derives
/// the base URI and current URI from the active <see cref="Microsoft.AspNetCore.Components.NavigationManager"/>, allowing components
/// that depend on <see cref="HttpContext"/> to render without throwing initialization errors.
/// </para>
/// </summary>
internal sealed class StaticNavigationManager : NavigationManager, IHostEnvironmentNavigationManager
{
    public StaticNavigationManager(IHttpContextAccessor httpContextAccessor)
    {
        var request = httpContextAccessor.HttpContext?.Request ?? throw new InvalidOperationException("HttpContext is not available.");

        var baseUri = $"{request.Scheme}://{request.Host}{request.PathBase}/";
        var uri = $"{baseUri.TrimEnd('/')}{request.Path}{request.QueryString}";

        Initialize(baseUri, uri);
    }

    protected override void NavigateToCore(string uri, bool forceLoad)
    {
        // Navigation is not supported during static rendering
    }

    void IHostEnvironmentNavigationManager.Initialize(string baseUri, string uri)
    {
        // Already initialized in constructor
    }
}