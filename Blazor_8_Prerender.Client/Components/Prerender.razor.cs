using Microsoft.AspNetCore.Components;

namespace Blazor_8_Prerender.Client.Components;

/// <summary>
/// Componente die anhand IBlazorRenderStateService erkennt, ob der Content Prerendert wurde
/// Zeigt je nach True / False andere RenderFragmente an
/// </summary>
public partial class Prerender : ComponentBase
{
    /// <summary>
    /// Content der angezeigt wird, wenn Blazor noch am laden ist (Prerender)
    /// </summary>
    [Parameter]
    public RenderFragment Server { get; set; }

    /// <summary>
    /// Content der angezeigt wird, wenn Blazor fertiggeladen ist
    /// </summary>
    [Parameter]
    public RenderFragment Client { get; set; }

    [Inject]
    private IBlazorRenderStateService BlazorRenderState { get; set; }
}