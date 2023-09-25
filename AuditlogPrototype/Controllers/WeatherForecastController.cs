using Audit.WebApi;
using Microsoft.AspNetCore.Mvc;

namespace AuditlogPrototype.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
   
    [HttpPost(Name = "GetWeatherForecast")]
    [AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeHeaders = true,IncludeRequestBody = true, IncludeResponseBody=true)]
    public IEnumerable<WeatherForecast> Get([FromBody] string peanut)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}