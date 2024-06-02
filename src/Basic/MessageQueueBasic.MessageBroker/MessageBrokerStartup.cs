using GreenPipes;
using MassTransit;
using MessageQueueBasic.MessageBroker.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace MessageQueueBasic.MessageBroker;

[ExcludeFromCodeCoverage]
public static partial class MessageBrokerStartup
{
    public static void AddMessageQueue(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddMassTransit(a =>
        {
            a.SetKebabCaseEndpointNameFormatter();

            #region Consumers

            // Add a single consumer
            a.AddConsumer<TestConsumer>(
                m => m.UseMessageRetry(r =>
                r.Interval(3, TimeSpan.FromMilliseconds(3000))
            ));

            #region outras formas de configruar consumidores

            // Add a single consumer
            //a.AddConsumer<TestConsumer>(typeof(TestConsumerDefinition));

            // Add a single consumer by type
            //a.AddConsumer(typeof(TestConsumer), typeof(TestConsumerDefinition));

            // Add all consumers in the specified assembly
            //a.AddConsumers(typeof(TestConsumer).Assembly);

            // Add all consumers in the namespace containing the specified type
            //a.AddConsumersFromNamespaceContaining<TestConsumer>();

            #endregion outras formas de configruar consumidores

            #endregion Consumers

            a.UsingRabbitMq((context, cfg) =>
            {
                var config = configuration.GetSection("QueueSettings").Get<HostConfiguration>();
                cfg.Host(config?.Host, config?.VirtualHost, h =>
                {
                    h.Username(config?.UserName);
                    h.Password(config?.Password);
                });

                cfg.ConfigureEndpoints(context);

            });
        });

        services.AddMassTransitHostedService();
    }
}
