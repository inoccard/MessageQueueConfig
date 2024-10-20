namespace BultiBus.MessageQueue.Messages;

public interface ISecondMessage
{
    public int Id { get; set; }
    public string Message { get; set; }
}