namespace ExamLib;

public abstract class ABaseHashSet<T>
{
    protected List<T>[] table;
    protected int size;
    protected IEqualityComparer<T>? comparer;

    protected ABaseHashSet(int capacity)
    {
        size = 0;
        table = new List<T>[capacity];
        for (int i = 0; i < capacity; i++)
        {
            table[i] = new List<T>();
        }

        comparer = null;
    }

    protected ABaseHashSet(int capacity, IEqualityComparer<T> comparer) : this(capacity)
    {
        this.comparer = comparer;
    }

    protected abstract bool PolicyDemandsResize { get; }
    protected abstract void Resize();
    protected abstract void Acquire(T x);
    protected abstract void Release(T x);

    private bool TableContains(int bucket, T x)
    {
        return comparer == null ? table[bucket].Contains(x) : table[bucket].Contains(x, comparer);
    }

    public bool Contains(T x)
    {
        Acquire(x);
        try
        {
            var myBucket = Math.Abs(x.GetHashCode() % table.Length); // обновить ?
            return TableContains(myBucket, x);
        }
        finally
        {
            Release(x);
        }
    }

    public bool Add(T x)
    {
        var result = false;
        Acquire(x);
        try
        {
            var myBucket = Math.Abs(x.GetHashCode() % table.Length);
            if (!TableContains(myBucket, x))
            {
                table[myBucket].Add(x);
                result = true;
                size++;
            }
        }
        finally
        {
            Release(x);
        }

        if (PolicyDemandsResize)
            Resize();
        return result;
    }

    public bool Remove(T x)
    {
        var result = false;
        Acquire(x);
        try
        {
            var myBucket = Math.Abs(x.GetHashCode() % table.Length);
            if (TableContains(myBucket, x))
            {
                table[myBucket].Remove(x);
                result = true;
                size--;
            }
        }
        finally
        {
            Release(x);
        }

        if (PolicyDemandsResize)
            Resize(); // может быть не надо?
        return result;
    }
}