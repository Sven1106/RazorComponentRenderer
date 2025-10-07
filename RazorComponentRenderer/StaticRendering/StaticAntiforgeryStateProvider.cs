using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace RazorComponentRenderer.StaticRendering;

/// <summary>
/// An <see cref="Microsoft.AspNetCore.Components.Forms.AntiforgeryStateProvider"/> implementation for static HTML rendering
/// that provides antiforgery tokens to Razor components during server-side rendering.
/// <para>
/// This provider integrates with ASP.NET Core's standard <see cref="IAntiforgery"/> service to generate
/// valid antiforgery tokens from the current <see cref="HttpContext"/>, enabling components with forms
/// (such as <c>&lt;EditForm&gt;</c> and <c>&lt;AntiforgeryToken/&gt;</c>) to render without errors.
/// All antiforgery configuration (cookie names, validation settings, etc.) is respected through
/// the underlying <see cref="IAntiforgery"/> service.
/// </para>
/// </summary>
internal sealed class StaticAntiforgeryStateProvider : AntiforgeryStateProvider
{
    private readonly IAntiforgery _antiforgery;
    private readonly IHttpContextAccessor _httpContextAccessor;

    // ReSharper disable once ConvertToPrimaryConstructor
    public StaticAntiforgeryStateProvider(
        IAntiforgery antiforgery,
        IHttpContextAccessor httpContextAccessor)
    {
        _antiforgery = antiforgery;
        _httpContextAccessor = httpContextAccessor;
    }

    public override AntiforgeryRequestToken GetAntiforgeryToken()
    {
        var httpContext = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HttpContext is not available.");
        var tokenSet = _antiforgery.GetAndStoreTokens(httpContext);
        var requestToken = tokenSet.RequestToken ?? throw new InvalidOperationException("Antiforgery token is not available.");
        return new AntiforgeryRequestToken(requestToken, tokenSet.FormFieldName);
    }
}