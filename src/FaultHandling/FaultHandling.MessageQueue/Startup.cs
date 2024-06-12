using FaultHandling.MessageQueue.Consumers;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sds.MessageQueue.Core;
using System.Diagnostics.CodeAnalysis;

namespace FaultHandling.MessageQueue;

[ExcludeFromCodeCoverage]
public static class Startup
{
    public static void RegisterMessageQueue(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(a =>
        {
            a.SetKebabCaseEndpointNameFormatter();

            #region Consumers

            // Add a single consumer
            a.AddConsumer<FailureTestConsumer>(
                m => m.UseMessageRetry(r =>
                r.Interval(3, TimeSpan.FromMilliseconds(3000))
            ));

            #endregion Consumers

            a.UsingRabbitMq((context, cfg) =>
            {
                var config = configuration.GetSection("FirstQueueSetting").Get<HostConfiguration>();
                cfg.Host(config.Host, config.VirtualHost, h =>
                {
                    h.Username(config.UserName);
                    h.Password(config.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
