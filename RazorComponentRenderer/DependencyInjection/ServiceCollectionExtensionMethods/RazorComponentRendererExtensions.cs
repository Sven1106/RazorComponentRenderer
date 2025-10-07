using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using RazorComponentRenderer.StaticRendering;

namespace RazorComponentRenderer.DependencyInjection.ServiceCollectionExtensionMethods;

public static class RazorComponentRendererExtensions
{
    public static IServiceCollection AddRazorComponentRenderer(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<HtmlRenderer>();
        services.AddScoped<RazorComponentRendererService>();

        // Required for navigation-dependent components
        services.AddScoped<NavigationManager, StaticNavigationManager>();
        services.AddScoped<IHostEnvironmentNavigationManager, StaticNavigationManager>();

        // Required for forms and antiforgery tokens
        services.AddAntiforgery();
        services.AddScoped<Microsoft.AspNetCore.Components.Forms.AntiforgeryStateProvider, StaticAntiforgeryStateProvider>();

        // Provide clear error messages for JavaScript interop calls
        services.AddScoped<IJSRuntime, StaticJSRuntime>();

        return services;
    }
}