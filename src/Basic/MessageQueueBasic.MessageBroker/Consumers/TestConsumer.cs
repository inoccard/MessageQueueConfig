using MassTransit;
using MessageQueueBasic.MessageBroker.Messages;
using Microsoft.Extensions.Logging;

namespace MessageQueueBasic.MessageBroker.Consumers;

public class TestConsumer(ILogger<TestConsumer> logger) : IConsumer<ITested>
{
    public async Task Consume(ConsumeContext<ITested> context)
    {
        var message = $"Mensagem recebida {context.Message.Name}";
        logger.LogInformation(message);
        await Task.FromResult(context.Message);
    }
}