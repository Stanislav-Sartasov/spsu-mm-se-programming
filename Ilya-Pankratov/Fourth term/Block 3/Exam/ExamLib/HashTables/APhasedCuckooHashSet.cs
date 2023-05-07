namespace ExamLib;

public abstract class APhasedCuckooHashSet<T>
{
    // list is semi-full.
    protected const int Threshold = 30;

    // list is full.
    protected const int ListSize = 60;

    // steps to relocate
    protected const int Limit = 75;

    // comparator for equality
    protected IEqualityComparer<T>? comparer;

    volatile protected int capacity;
    volatile protected List<T>[,] table;

    public APhasedCuckooHashSet(int size)
    {
        capacity = size;
        table = new List<T>[2, capacity];

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < capacity; j++)
            {
                table[i, j] = new List<T>(ListSize);
            }
        }

        comparer = null;
    }

    public APhasedCuckooHashSet(int size, IEqualityComparer<T> comparer) : this(size)
    {
        this.comparer = comparer;
    }

    private bool TableContains(List<T> list, T x)
    {
        return comparer == null ? list.Contains(x) : list.Contains(x, comparer);
    }

    private bool TableRemove(List<T> list, T value)
    {
        if (comparer != null)
        {
            var index = list.FindIndex(x => comparer.Equals(x, value));

            if (index == -1)
            {
                return false;
            }
            else
            {
                list.RemoveAt(index);
                return true;
            }
        }
        else
        {
            return list.Remove(value);
        }
    }

    protected int Hash0(T x)
    {
        var hash = x.GetHashCode();
        hash = ((hash >> 16) ^ hash) * 0x45d9f3b;
        hash = ((hash >> 16) ^ hash) * 0x45d9f3b;
        return Math.Abs((hash >> 16) ^ hash);
    }

    protected int Hash1(T x)
    {
        var hash = x.GetHashCode();
        var constant = 0x27d4eb2d;
        hash = hash ^ 61 ^ (hash >>> 16);
        hash += hash << 3;
        hash ^= hash >>> 4;
        hash *= constant;
        hash ^= hash >>> 15;
        return Math.Abs(hash);
    }

    public bool Contains(T x)
    {
        Acquire(x);
        try
        {
            var set0 = table[0, Hash0(x) % capacity];
            if (TableContains(set0, x))
            {
                return true;
            }
            else
            {
                var set1 = table[1, Hash1(x) % capacity];
                if (TableContains(set1, x))
                {
                    return true;
                }
            }

            return false;
        }
        finally
        {
            Release(x);
        }
    }

    protected abstract void Acquire(T x);
    protected abstract void Release(T x);
    protected abstract void Resize();

    public bool Remove(T x)
    {
        Acquire(x);
        try
        {
            var set0 = table[0, Hash0(x) % capacity];
            if (TableContains(set0, x))
            {
                TableRemove(set0, x);
                return true;
            }
            else
            {
                var set1 = table[1, Hash1(x) % capacity];
                if (TableContains(set1, x))
                {
                    TableRemove(set1, x);
                    return true;
                }
            }

            return false;
        }
        finally
        {
            Release(x);
        }
    }

    public bool Add(T x)
    {
        Acquire(x);
        int h0 = Hash0(x) % capacity, h1 = Hash1(x) % capacity;
        int i = -1, h = -1;
        var mustResize = false;
        try
        {
            if (Contains(x)) return false;
            var set0 = table[0, h0];
            var set1 = table[1, h1];

            if (set0.Count < Threshold)
            {
                set0.Add(x);
                return true;
            }
            else if (set1.Count < Threshold)
            {
                set1.Add(x);
                return true;
            }
            else if (set0.Count < ListSize)
            {
                set0.Add(x);
                i = 0;
                h = h0;
            }
            else if (set1.Count < ListSize)
            {
                set1.Add(x);
                i = 1;
                h = h1;
            }
            else
            {
                mustResize = true;
            }
        }
        finally
        {
            Release(x);
        }

        if (mustResize)
        {
            Resize();
            Add(x);
        }
        else if (!Relocate(i, h))
        {
            Resize();
        }

        return true; // x must have been present
    }

    protected bool Relocate(int i, int hi)
    {
        var hj = 0;
        var j = 1 - i;
        for (var round = 0; round < Limit; round++)
        {
            var iSet = table[i, hi];
            var y = iSet[0];
            switch (i)
            {
                case 0:
                    hj = Hash1(y) % capacity;
                    break;
                case 1:
                    hj = Hash0(y) % capacity;
                    break;
            }

            Acquire(y);
            var jSet = table[j, hj];
            try
            {
                if (TableRemove(iSet, y))
                {
                    if (jSet.Count < Threshold)
                    {
                        jSet.Add(y);
                        return true;
                    }
                    else if (jSet.Count < ListSize)
                    {
                        jSet.Add(y);
                        i = 1 - i;
                        hi = hj;
                        j = 1 - j;
                    }
                    else
                    {
                        iSet.Add(y);
                        return false;
                    }
                }
                else if (iSet.Count >= Threshold)
                {
                    continue;
                }
                else
                {
                    return true;
                }
            }
            finally
            {
                Release(y);
            }
        }

        return false;
    }
}