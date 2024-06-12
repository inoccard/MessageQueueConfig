using FaultHandling.MessageQueue.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FaultHandling.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FaultHandlingController(
    ILogger<FaultHandlingController> logger,
    IPublishEndpoint publishEndpoint) : ControllerBase
{
    /// <summary>
    /// Publica uma mensagem na fila
    /// </summary>
    /// <returns></returns>
    [HttpPost("send-message")]
    public IActionResult SendMessage()
    {
        publishEndpoint.Publish<IFailureTest>(new
        {
            Id = 1,
            Name = "Inocencio Cardoso"
        });

        logger.LogInformation("Mensagem publicada");

        return Ok();
    }
}
