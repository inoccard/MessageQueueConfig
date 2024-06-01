namespace BultiBus.MessageQueue;

public class HostConfiguration
{
    public required string Host { get; set; }
    public required ushort Port { get; set; }
    public required string VirtualHost { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
