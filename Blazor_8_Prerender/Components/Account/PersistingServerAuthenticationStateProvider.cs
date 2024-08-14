using Blazor_8_Prerender.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;

namespace Blazor_8_Prerender.Components.Account
{
    // This is a server-side AuthenticationStateProvider that uses PersistentComponentState to flow the
    // authentication state to the client which is then fixed for the lifetime of the WebAssembly application.
    internal sealed class PersistingServerAuthenticationStateProvider : ServerAuthenticationStateProvider, IDisposable
    {
        private readonly PersistentComponentState state;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IdentityOptions options;

        private readonly PersistingComponentStateSubscription subscription;

        private Task<AuthenticationState>? authenticationStateTask;

        public PersistingServerAuthenticationStateProvider(
            PersistentComponentState persistentComponentState,
            IOptions<IdentityOptions> optionsAccessor,
            IHttpContextAccessor contextAccessor)
        {
            state = persistentComponentState;
            _contextAccessor = contextAccessor;
            options = optionsAccessor.Value;

            AuthenticationStateChanged += OnAuthenticationStateChanged;
            subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
        }

        /// <summary>
        /// Wird nur ausgelöst während des Prerendering,
        /// wenn der Authentication State mit<CascadingAuthenticationState>
        /// angegeben wird.
        /// </summary>
        /// <returns></returns>
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (this._contextAccessor.HttpContext.Request.Path.StartsWithSegments("/auth"))
            {
                /*
                 * Hier kann Logik eingebaut werden
                 * die den Auth State des Benutzers während Prerendering
                 * ändert. Z.B. Kann dies von Custom Cookies abhängen oder
                 * von einer bestimmten Route. Mit dem IHttpConextAccessor kann
                 * der HttpContext über den Konstruktor eingefügt werden um so
                 * Zugriff auf Cookies oder Routen zu erhalten.
                */

                // Für Test Zwecke, kann ein nicht Authorisierter Benutzer zurückgegeben werden.
                ////return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
            }

            return base.GetAuthenticationStateAsync();
        }

        private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            authenticationStateTask = task;
        }

        private async Task OnPersistingAsync()
        {
            if (authenticationStateTask is null)
            {
                throw new UnreachableException($"Authentication state not set in {nameof(OnPersistingAsync)}().");
            }

            var authenticationState = await authenticationStateTask;
            var principal = authenticationState.User;

            if (principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirst(options.ClaimsIdentity.UserIdClaimType)?.Value;
                var email = principal.FindFirst(options.ClaimsIdentity.EmailClaimType)?.Value;

                if (userId != null && email != null)
                {
                    state.PersistAsJson(nameof(UserInfo), new UserInfo
                    {
                        UserId = userId,
                        Email = email,
                    });
                }
            }
        }

        public void Dispose()
        {
            subscription.Dispose();
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}
