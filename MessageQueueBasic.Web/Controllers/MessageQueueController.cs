using MassTransit;
using MessageQueueBasic.Domain.Models;
using MessageQueueBasic.MessageBroker.Messages;
using Microsoft.AspNetCore.Mvc;

namespace MessageQueueBasic.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageQueueController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<MessageQueueController> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageQueueController(ILogger<MessageQueueController> logger, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    /// <summary>
    /// Publica uma mensagem na fila
    /// </summary>
    /// <returns></returns>
    [HttpPost("send-message")]
    public IActionResult SendMessage()
    {
        _publishEndpoint.Publish<ITested>(new
        {
            Id = 1,
            Name = "Inocencio Cardoso"
        });
        return Ok();
    }
}
