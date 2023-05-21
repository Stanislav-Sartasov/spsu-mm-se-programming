namespace Core.Network;

public class NodeMessage
{
    public string Name { get; set; }
    public DateTime SentTime { get; set; }
    public DateTime ReceiptTime { get; set; }
    public string IpAddress { get; set; }
    public Type Type { get; set; }
}