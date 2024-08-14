using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Blazor_8_Prerender.Client.Pages.Weather;

namespace Blazor_8_Prerender.Controller;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
    public async Task<ActionResult<WeatherForecast[]>> Get()
    {
        // Simulate asynchronous loading to demonstrate a loading indicator
        await Task.Delay(500);

        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToArray();

        return this.Ok(forecasts);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Update()
    {
        await Task.Delay(500);

        Console.WriteLine("Log");

        return Ok();
    }
}