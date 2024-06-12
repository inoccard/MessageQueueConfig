using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FaultHandling.MessageQueue.Consumers;

public class FailureTestConsumer(ILogger<FailureTestConsumer> logger) : IConsumer<FailureTestConsumer>
{
    public async Task Consume(ConsumeContext<FailureTestConsumer> context)
    {
        logger.LogInformation($"mensagem recebida: {JsonConvert.SerializeObject(context.Message)}");
        await Task.FromResult(context.Message);
    }
}

