using Blazor_8_Prerender.Client.Pages;
using static Blazor_8_Prerender.Client.Pages.Weather;

namespace Blazor_8_Prerender.Controller;

/// <summary>
/// Abstraktion des Weather Service,
/// um Daten während des Prerendering
/// direkt vom Server zu holen.
/// </summary>
public interface IWeatherService
{
    Task<WeatherForecast[]> GetAsync();
    Task UpdateAsync();
}