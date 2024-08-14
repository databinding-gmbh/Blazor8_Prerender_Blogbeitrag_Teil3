
using Blazor_8_Prerender.Client.Components;

namespace Blazor_8_Prerender.Components;

/// <summary>
/// Server Service für das abfragen des Render
/// State.
/// </summary>
public class ServerBlazorRenderStateService : IBlazorRenderStateService
{
    private readonly HttpContext? httpContext;

    /// <summary>
    /// Ctor mit IHttpContextAccessor
    /// </summary>
    /// <param name="contextAccessor"></param>
    public ServerBlazorRenderStateService(IHttpContextAccessor contextAccessor)
    {
        this.httpContext = contextAccessor.HttpContext;
    }

    /// <summary>
    /// Ob der Render State sich im Prerender befindet.
    /// Kann verwendet werden um Inhalte nur im PrerenderState
    /// oder nur im WASM State anzuzeigen.
    /// </summary>
    public bool IsPrerender => this.httpContext is not null
                               && !this.httpContext.Response.HasStarted
                               && !this.httpContext.Request.Path.StartsWithSegments("/Account");
}