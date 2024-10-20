using System.Diagnostics.CodeAnalysis;
using BultiBus.MessageQueue.Bus;
using BultiBus.MessageQueue.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BultiBus.MessageQueue;

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
            a.AddConsumer<FirstConsumer>(
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

        services.AddMassTransit<ISecondBus>(a =>
        {
            a.SetKebabCaseEndpointNameFormatter();

            #region Consumers

            // Add a single consumer
            a.AddConsumer<SecondConsumer>(
                m => m.UseMessageRetry(r =>
                    r.Interval(3, TimeSpan.FromMilliseconds(3000))
                ));

            #endregion Consumers

            a.UsingRabbitMq((context, cfg) =>
            {
                var config = configuration.GetSection("SecondQueueSetting").Get<HostConfiguration>();
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