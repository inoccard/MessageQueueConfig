using FaultHandling.MessageQueue.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FaultHandling.MessageQueue.Consumers;

public class FailureTestConsumer(ILogger<FailureTestConsumer> logger) : IConsumer<IFailureTest>
{
    public Task Consume(ConsumeContext<IFailureTest> context)
    {
        logger.LogInformation($"mensagem recebida: {JsonConvert.SerializeObject(context.Message)}");
        
        throw new NotImplementedException("teste falhou");
    }
}

