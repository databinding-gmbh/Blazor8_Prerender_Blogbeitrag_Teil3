namespace Blazor_8_Prerender.Client.Weather.Services;

/// <summary>
/// Abstraktion des Weather Service,
/// um Daten während des Prerendering
/// direkt vom Server zu holen.
/// </summary>
public interface IWeatherService
{
    Task<Pages.Weather.WeatherForecast[]> GetAsync();
    Task UpdateAsync();
}