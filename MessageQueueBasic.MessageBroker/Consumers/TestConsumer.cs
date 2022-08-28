using MassTransit;
using MessageQueueBasic.MessageBroker.Messages;
using Microsoft.Extensions.Logging;

namespace MessageQueueBasic.MessageBroker.Consumers;

public class TestConsumer : IConsumer<ITested>
{
    readonly ILogger<TestConsumer> _logger;

    public TestConsumer(ILogger<TestConsumer> logger)
    {
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<ITested> context)
    {
        _logger.LogInformation("Mensagem recebida", context.Message.Name);
        await Task.FromResult(context.Message);
    }
}
