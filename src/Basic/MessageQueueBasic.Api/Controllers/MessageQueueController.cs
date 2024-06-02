using MassTransit;
using MessageQueueBasic.MessageBroker.Messages;
using Microsoft.AspNetCore.Mvc;

namespace MessageQueueBasic.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageQueueController(
    ILogger<MessageQueueController> logger,
    IPublishEndpoint publishEndpoint) : ControllerBase
{

    /// <summary>
    /// Publica uma mensagem na fila
    /// </summary>
    /// <returns></returns>
    [HttpPost("send-message")]
    public IActionResult SendMessage()
    {
        publishEndpoint.Publish<ITested>(new
        {
            Id = 1,
            Name = "Inocencio Cardoso"
        });

        logger.LogInformation("Mensagem publicada");

        return Ok();
    }
}
