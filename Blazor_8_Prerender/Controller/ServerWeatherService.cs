using Blazor_8_Prerender.Client.Pages;
using static Blazor_8_Prerender.Client.Pages.Weather;

namespace Blazor_8_Prerender.Controller;

/// <summary>
/// Service für das Abrufen der Daten
/// direkt. (Kann durch Repository z.B. EF Core ersetzt werden)
/// Wird beim Prerendering der Weatherseite aufgerufen.
/// </summary>
public class ServerWeatherService : IWeatherService
{
    public async Task<WeatherForecast[]> GetAsync()
    {
        await Task.Delay(500);

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToArray();

        return forecasts;
    }

    /// <summary>
    /// Methode sollte nie während des Prerendering
    /// aufgerufen werden. Für Server Render Mode
    /// kann hier aber auch eine Implementierung erfolgen.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public Task UpdateAsync()
    {
        throw new NotSupportedException();
    }
}