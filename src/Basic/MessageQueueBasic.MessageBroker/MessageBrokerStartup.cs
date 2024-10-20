using System.Diagnostics.CodeAnalysis;
using MassTransit;
using MessageQueueBasic.MessageBroker.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageQueueBasic.MessageBroker;

[ExcludeFromCodeCoverage]
public static class MessageBrokerStartup
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
    }
}