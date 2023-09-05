namespace Core.Network;

public class NodeMessage
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IpAddress { get; set; }
    public int Port { get; set; }
    public DateTime SentTime { get; set; }
    public DateTime ReceiptTime { get; set; }
    public Type Type { get; set; }
}