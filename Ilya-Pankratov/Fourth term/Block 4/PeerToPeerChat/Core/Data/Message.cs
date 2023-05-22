namespace Core.Chat;

public class Message
{
    public string PeerName { get; set; }
    public DateTime SentTime { get; set; }
    public string Content { get; set; }
}