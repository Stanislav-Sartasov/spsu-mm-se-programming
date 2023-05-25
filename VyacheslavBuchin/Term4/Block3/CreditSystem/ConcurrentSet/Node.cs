namespace ConcurrentSet;

public class Node<T>
{
    public T Value;
    public int Key;
    public Node<T> Next;
    private Mutex _mtx = new(false);
    public void Lock() => _mtx.WaitOne();
    public void Unlock() => _mtx.ReleaseMutex();
    public bool Marked = false;

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
