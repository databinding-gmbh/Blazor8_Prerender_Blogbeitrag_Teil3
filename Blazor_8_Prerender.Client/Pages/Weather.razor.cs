using System.Net.Http.Json;
using Blazor_8_Prerender.Controller;
using Microsoft.AspNetCore.Components;

namespace Blazor_8_Prerender.Client.Pages;

public partial class Weather
{
    private WeatherForecast[]? forecasts;
    private PersistingComponentStateSubscription persistingState;

    [Inject]
    protected PersistentComponentState ApplicationState { get; set; }

    [Inject]
    private IWeatherService WeatherService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        this.persistingState = this.ApplicationState.RegisterOnPersisting(this.PersistContent);

        if (this.ApplicationState.TryTakeFromJson<WeatherForecast[]>("weatherData", out forecasts))
        {
        }
        else
        {
            this.forecasts = await this.WeatherService.GetAsync();
        }
    }

    private Task PersistContent()
    {
        this.ApplicationState.PersistAsJson("weatherData", forecasts);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Klasse in Models oder Shared Projekt verschieben
    /// </summary>
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    private async Task Update()
    {
        await this.WeatherService.UpdateAsync();
    }
}