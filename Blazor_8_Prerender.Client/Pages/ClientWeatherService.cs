using System.Net.Http.Json;
using Blazor_8_Prerender.Client.Pages;

namespace Blazor_8_Prerender.Controller;

/// <summary>
/// Service für das Abrufen von Wetterdaten über
/// den Http Client
/// </summary>
public class ClientWeatherService : IWeatherService
{
    private readonly HttpClient client;

    public ClientWeatherService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Weather.WeatherForecast[]> GetAsync()
    {
        var data = await this.client.GetFromJsonAsync<Weather.WeatherForecast[]>("https://localhost:7272/api/Weather");

        return data;
    }

    public async Task UpdateAsync()
    {
        await this.client.PostAsync("api/Weather", null);
    }
}