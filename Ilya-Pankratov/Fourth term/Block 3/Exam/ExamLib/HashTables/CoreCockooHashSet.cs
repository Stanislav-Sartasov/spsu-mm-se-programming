namespace ExamLib;

public class CoreCockooHashSet<T> : APhasedCuckooHashSet<T>, IHashTable<T>
{
    private Mutex mutex = new();

    public CoreCockooHashSet(int size) : base(size)
    {
    }

    public CoreCockooHashSet(int size, IEqualityComparer<T> comparer) : base(size, comparer)
    {
    }

    protected override void Acquire(T x)
    {
        mutex.WaitOne();
    }

    protected override void Release(T x)
    {
        mutex.ReleaseMutex();
    }

    protected override void Resize()
    {
        var oldCapacity = capacity;
        mutex.WaitOne();
        try
        {
            if (capacity != oldCapacity)
            {
                return;
            }

            var oldTable = table;
            capacity = 2 * capacity;
            table = new List<T>[2, capacity];

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < capacity; j++)
                {
                    table[i, j] = new List<T>(ListSize);
                }
            }

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < oldCapacity; j++)
                {
                    foreach (var z in oldTable[i, j])
                    {
                        Add(z);
                    }
                }
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    public static IHashTable<T> GetInstance(int capacity)
    {
        return new CoreCockooHashSet<T>(capacity);
    }

    public static IHashTable<T> GetInstance(int capacity, IEqualityComparer<T> comparer)
    {
        return new CoreCockooHashSet<T>(capacity, comparer);
    }
}