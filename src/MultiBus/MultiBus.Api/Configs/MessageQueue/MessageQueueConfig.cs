using BultiBus.MessageQueue;

namespace MultiBus.Api.Configs.MessageQueue;

public static class MessageQueueConfig
{
    public static void AddMessageQueue(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<HostConfiguration>(configuration.GetSection(nameof(HostConfiguration)));

        services.RegisterMessageQueue(configuration);
    }
}
