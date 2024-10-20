using BultiBus.MessageQueue.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BultiBus.MessageQueue.Consumers;

internal class FirstConsumer(ILogger<FirstConsumer> logger) : IConsumer<IFirstMessage>
{
    public async Task Consume(ConsumeContext<IFirstMessage> context)
    {
        logger.LogInformation($"primeira mensagem recebida: {JsonConvert.SerializeObject(context.Message)}");
        await Task.FromResult(context.Message);
    }
}