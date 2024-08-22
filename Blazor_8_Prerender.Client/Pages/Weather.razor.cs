using System.Net.Http.Json;
using Blazor_8_Prerender.Controller;
using Microsoft.AspNetCore.Components;

namespace Blazor_8_Prerender.Client.Pages;

public partial class Weather : IDisposable
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

        // Versucht die Daten aus dem ApplicationState zu holen.
        // Falls diese nicht vorhanden sind, müssen die Daten vom Server abgerufen werden.
        if (this.ApplicationState.TryTakeFromJson("weatherData", out forecasts))
        {
        }
        else
        {
            this.forecasts = await this.WeatherService.GetAsync();
        }
    }

    /// <summary>
    /// Methode schreibt alle Relevanten
    /// Daten während des Prerendering in den ApplicationState.
    /// </summary>
    /// <returns></returns>
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

    public void Dispose() => persistingState.Dispose();
}