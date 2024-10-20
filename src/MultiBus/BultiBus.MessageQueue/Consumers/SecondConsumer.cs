using BultiBus.MessageQueue.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BultiBus.MessageQueue.Consumers;

internal class SecondConsumer(ILogger<SecondConsumer> logger) : IConsumer<ISecondMessage>
{
    public async Task Consume(ConsumeContext<ISecondMessage> context)
    {
        logger.LogInformation($"segunda mensagem recebida: {JsonConvert.SerializeObject(context.Message)}");
        await Task.FromResult(context.Message);
    }
}