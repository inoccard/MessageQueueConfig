using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace MessageQueueBasic.MessageBroker.Consumers.Definitions;
public class TestConsumerDefinition : ConsumerDefinition<TestConsumer>
{
    /// <summary>
    /// Constructor
    /// </summary>
    public TestConsumerDefinition()
    {
        // override the default endpoint name
        EndpointName = "test-service";

        // limit the number of messages consumed concurrently
        // this applies to the consumer only, not the endpoint
        ConcurrentMessageLimit = 8;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TestConsumer> consumerConfigurator)
    {
        // configure message retry with millisecond intervals
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));

        // use the outbox to prevent duplicate events from being published
        endpointConfigurator.UseInMemoryOutbox();
    }
}
