namespace ConcurrentSet;

public interface ISet<in T>
{
    bool Add(T item);
    bool Remove(T item);
    bool Contains(T item);
    int Count();
}
