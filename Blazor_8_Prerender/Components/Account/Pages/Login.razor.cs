using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace Blazor_8_Prerender.Components.Account.Pages;

public partial class Login
{
    private string? errorMessage;

    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;

    [SupplyParameterFromForm] private InputModel Input { get; set; } = new();

    [SupplyParameterFromQuery] private string? ReturnUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (HttpMethods.IsGet(this.HttpContext.Request.Method))
        {
            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }
    }

    public async Task LoginUser()
    {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result =
            await this.SignInManager.PasswordSignInAsync(
                this.Input.Email,
                this.Input.Password,
                this.Input.RememberMe,
                                                         lockoutOnFailure: false);
        if (result.Succeeded)
        {
            this.Logger.LogInformation("User logged in.");
            this.RedirectManager.RedirectTo(this.ReturnUrl);
        }
        else if (result.RequiresTwoFactor)
        {
            this.RedirectManager.RedirectTo(
                "Account/LoginWith2fa",
                new() { ["returnUrl"] = this.ReturnUrl, ["rememberMe"] = this.Input.RememberMe });
        }
        else if (result.IsLockedOut)
        {
            this.Logger.LogWarning("User account locked out.");
            this.RedirectManager.RedirectTo("Account/Lockout");
        }
        else
        {
            this.errorMessage = "Error: Invalid login attempt.";
        }
    }

    private sealed class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
    }
}