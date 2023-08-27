namespace DataStructures;

public class Node<T>
{
    public int Key;
    public ILock Lock = new SimpleLock();
    public volatile bool Marked = false;
    public Node<T> Next;
    public T Value;

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