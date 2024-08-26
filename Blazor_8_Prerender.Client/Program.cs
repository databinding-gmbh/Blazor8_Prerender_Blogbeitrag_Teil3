using Blazor_8_Prerender.Client;
using Blazor_8_Prerender.Client.Components;
using Blazor_8_Prerender.Client.Weather.Services;
using Blazor_8_Prerender.Controller;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//builder.Services.AddCascadingAuthenticationState();

builder.Services.AddHttpClient(
    "API", (sp, client) =>
    {
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    });

builder.Services.AddTransient(
    sp => sp.GetRequiredService<IHttpClientFactory>()
        .CreateClient("API"));

builder.Services.AddAuthorizationCore();

builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped<IBlazorRenderStateService, WasmBlazorRenderStateService>();

builder.Services.AddSingleton<IWeatherService, ClientWeatherService>();

await builder.Build().RunAsync();
