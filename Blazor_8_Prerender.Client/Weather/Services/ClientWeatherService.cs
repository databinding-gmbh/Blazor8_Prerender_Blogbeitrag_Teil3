using System.Net.Http.Json;

namespace Blazor_8_Prerender.Client.Weather.Services;

/// <summary>
/// Service für das Abrufen von Wetterdaten über
/// den Http Client.
/// </summary>
public class ClientWeatherService : IWeatherService
{
    private readonly HttpClient client;

    public ClientWeatherService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Pages.Weather.WeatherForecast[]> GetAsync()
    {
        var data = await this.client.GetFromJsonAsync<Pages.Weather.WeatherForecast[]>("/api/Weather");

        return data;
    }

    public async Task UpdateAsync()
    {
        await this.client.PostAsync("api/Weather", null);
    }
}