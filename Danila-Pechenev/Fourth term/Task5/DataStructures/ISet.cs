namespace DataStructures;

public interface ISet<T>
{
    public bool Add(T item);

    public bool Remove(T item);

    public bool Contains(T item);

    public int Count();
}