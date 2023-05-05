namespace ExamLib;

public class StripedHashSet<T> : ABaseHashSet<T>, IHashTable<T>
{
    readonly Mutex[] locks;

    public StripedHashSet(int capacity) : base(capacity)
    {
        locks = new Mutex[capacity];
        for (int i = 0; i < locks.Length; i++)
        {
            locks[i] = new Mutex();
        }
    }

    public StripedHashSet(int capacity, IEqualityComparer<T> comparer) : this(capacity)
    {
        this.comparer = comparer;
    }

    protected override bool PolicyDemandsResize => size / table.Length > 4; // ???

    protected override void Acquire(T x)
    {
        locks[Math.Abs(x.GetHashCode() % locks.Length)].WaitOne();
    }

    protected override void Release(T x)
    {
        locks[Math.Abs(x.GetHashCode() % locks.Length)].ReleaseMutex();
    }

    protected override void Resize()
    {
        var oldCapacity = table.Length;

        foreach (var m in locks)
        {
            m.WaitOne();
        }

        try
        {
            if (oldCapacity != table.Length)
            {
                return;
            }

            var newCapacity = 2 * oldCapacity;

            var oldTable = table;
            table = new List<T>[newCapacity];
            for (var i = 0; i < newCapacity; i++)
                table[i] = new List<T>();
            foreach (var bucket in oldTable)
            {
                foreach (var x in bucket)
                {
                    table[x.GetHashCode() % table.Length].Add(x);
                }
            }
        }
        finally
        {
            foreach (var m in locks)
            {
                m.ReleaseMutex();
            }
        }
    }

    public static IHashTable<T> GetInstance(int capacity)
    {
        return new StripedHashSet<T>(capacity);
    }

    public static IHashTable<T> GetInstance(int capacity, IEqualityComparer<T> comparer)
    {
        return new StripedHashSet<T>(capacity, comparer);
    }
}