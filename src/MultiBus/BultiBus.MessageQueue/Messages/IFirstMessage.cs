namespace BultiBus.MessageQueue.Messages;

public interface IFirstMessage
{
    public int Id { get; set; }
    public string Message { get; set; }
}