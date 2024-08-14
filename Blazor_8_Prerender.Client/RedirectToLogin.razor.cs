using Microsoft.AspNetCore.Components;

namespace Blazor_8_Prerender.Client;

public partial class RedirectToLogin
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        NavigationManager.NavigateTo($"Account/Login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}",
            forceLoad: true);
    }
}