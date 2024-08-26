using Blazor_8_Prerender.Client.Weather.Pages;
using Blazor_8_Prerender.Client.Weather.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Blazor_8_Prerender.Client.Pages.Weather;

namespace Blazor_8_Prerender.Controller;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<ActionResult<Weather.WeatherForecast[]>> Get()
    {
        var data = await this._weatherService.GetAsync();

        return this.Ok(data);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Update()
    {
        await Task.Delay(500);

        Console.WriteLine("Ist gemacht");

        return Ok();
    }
}