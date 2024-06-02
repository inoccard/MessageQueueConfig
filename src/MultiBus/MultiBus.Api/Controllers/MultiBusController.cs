using BultiBus.MessageQueue.Bus;
using BultiBus.MessageQueue.Messages;
using MassTransit;
using MassTransit.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace MultiBus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MultiBusController(
    ILogger<MultiBusController> logger,
    IPublishEndpoint publishFirstBus,
    Bind<ISecondBus, IPublishEndpoint> publishSecondBus) : ControllerBase
    {
        [HttpPost("publish-first")]
        public async Task<ActionResult> PublishFirst()
        {
            await publishFirstBus.Publish<IFirstMessage>(
                new
                {
                    Id = 1,
                    Message = "esta é a primeira mensagem"
                });

            logger.LogInformation($"{nameof(PublishFirst)} mensagem enviada");

            return Ok();
        }

        [HttpPost("publish-second")]
        public async Task<ActionResult> PublishSecond()
        {
            await publishSecondBus.Value.Publish<ISecondMessage>(
                new
                {
                    Id = 1,
                    Message = "esta é a segunda mensagem"
                });

            logger.LogInformation($"{nameof(PublishSecond)} mensagem enviada");

            return Ok();
        }
    }
}
