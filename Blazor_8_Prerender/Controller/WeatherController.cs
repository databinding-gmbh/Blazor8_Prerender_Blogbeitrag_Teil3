using Blazor_8_Prerender.Client.Weather.Pages;
using Blazor_8_Prerender.Client.Weather.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor_8_Prerender.Controller;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        this.weatherService = weatherService;
    }

    public async Task<ActionResult<Weather.WeatherForecast[]>> Get()
    {
        var data = await this.weatherService.GetAsync();

        return this.Ok(data);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Update()
    {
        await Task.Delay(500);

        Console.WriteLine("Ist gemacht");

        return this.Ok();
    }
}