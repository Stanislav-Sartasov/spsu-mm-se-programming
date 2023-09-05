namespace ProducerConsumer;

public class Buffer<T>
{
    private readonly List<T> store = new();

    public bool IsEmpty => store.Count == 0;

    public T Pop()
    {
        var item = store[0];
        store.RemoveAt(0);
        return item;
    }

    public void Push(T item)
    {
        store.Add(item);
    }

}