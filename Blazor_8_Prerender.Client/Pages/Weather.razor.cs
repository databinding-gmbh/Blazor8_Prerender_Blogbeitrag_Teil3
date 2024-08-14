using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace Blazor_8_Prerender.Client.Pages;

public partial class Weather
{
    private WeatherForecast[]? forecasts;

    [Inject] private HttpClient httpClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        this.forecasts =
            await this.httpClient.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7272/api/Weather");
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
        await this.httpClient.PostAsync("api/Weather", null);
    }
}