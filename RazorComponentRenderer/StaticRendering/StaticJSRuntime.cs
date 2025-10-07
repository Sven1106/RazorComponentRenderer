using Microsoft.JSInterop;

namespace RazorComponentRenderer.StaticRendering;

/// <summary>
/// An <see cref="IJSRuntime"/> implementation for static HTML rendering that throws clear exceptions
/// when JavaScript interop is attempted during server-side component rendering.
/// <para>
/// This implementation prevents JavaScript interop calls from executing during static rendering,
/// providing clear error messages to guide developers toward static-rendering-compatible patterns.
/// Components using JavaScript interop should wrap calls in try-catch blocks or use conditional
/// logic to detect static rendering contexts.
/// </para>
/// </summary>
// ReSharper disable once InconsistentNaming
internal sealed class StaticJSRuntime : IJSRuntime
{
    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
    {
        throw new NotSupportedException($"JavaScript interop call '{identifier}' is not supported during static rendering.");
    }

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
    {
        throw new NotSupportedException($"JavaScript interop call '{identifier}' is not supported during static rendering.");
    }
}