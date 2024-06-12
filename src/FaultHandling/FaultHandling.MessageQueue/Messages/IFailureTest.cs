namespace FaultHandling.MessageQueue.Messages;

public interface IFailureTest
{
    public int Id { get; set; }
    public string Name { get; set; }
}
