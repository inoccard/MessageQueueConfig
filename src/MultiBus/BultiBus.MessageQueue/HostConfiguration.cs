namespace BultiBus.MessageQueue;

public class HostConfiguration
{
    public required string Host { get; init; }
    public required ushort Port { get; init; }
    public required string VirtualHost { get; init; }
    public required string UserName { get; init; }
    public required string Password { get; init; }
}
