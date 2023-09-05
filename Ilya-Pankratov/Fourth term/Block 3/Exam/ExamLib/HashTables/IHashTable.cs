namespace ExamLib;

public interface IHashTable<T>
{
    public bool Contains(T x);
    public bool Add(T x);
    public bool Remove(T x);
    public static abstract IHashTable<T> GetInstance(int capacity);
    public static abstract IHashTable<T> GetInstance(int capacity, IEqualityComparer<T> comparer);
}