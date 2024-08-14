using Blazor_8_Prerender.Client.Pages;
using static Blazor_8_Prerender.Client.Pages.Weather;

namespace Blazor_8_Prerender.Controller;

public interface IWeatherService
{
    Task<WeatherForecast[]> GetAsync();
    Task UpdateAsync();
}