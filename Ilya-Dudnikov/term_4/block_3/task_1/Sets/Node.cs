namespace Sets;

public class Node<T>
{
    public T Value;
    public int Key;
    public Node<T> Next;
    public volatile bool Marked = false;
    private Mutex mutex = new();

    public void Lock()
    {
        mutex.WaitOne();
    }

    public void Unlock()
    {
        mutex.ReleaseMutex();
    }

    public Node(int key)
    {
        Key = key;
    }

    public Node(T value)
    {
        Value = value;
        Key = Value.GetHashCode();
    }

    public override int GetHashCode()
    {
        return Key;
    }
}