using Microsoft.AspNetCore.Components;

namespace Blazor_8_Prerender.Client.Components;

public partial class RedirectToLogin
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    /// <summary>
    /// Wird aufgerufen von Routes.razor
    /// wenn der Benutzer nicht berechtigt ist.
    /// </summary>
    protected override void OnInitialized()
    {
        this.NavigationManager.NavigateTo($"Account/Login?returnUrl={Uri.EscapeDataString(this.NavigationManager.Uri)}",
                                          forceLoad: true);
    }
}