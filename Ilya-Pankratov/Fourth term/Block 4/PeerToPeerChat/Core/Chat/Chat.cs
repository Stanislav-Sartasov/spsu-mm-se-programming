namespace Core.Chat;

public class Chat<T> : IChat<T>
{
    private List<T> messages { get; }
    private object locker;

    public Chat()
    {
        locker = new object();
        messages = new List<T>();
    }

    public void SaveMessage(T message)
    {
        lock (locker)
        {
            messages.Add(message);
        }
    }

    public IEnumerable<T> Get(Predicate<T> predicate)
    {
        return messages.FindAll(predicate);
    }

    public IEnumerable<T> GetAll()
    {
        return new List<T>(messages);
    }
}