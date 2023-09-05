namespace ExamSystem.ConcurrentCollections;

internal class StripedCuckooHashSet<T> where T : Tuple<long, long>
{
    // list is semi-full
    private const int Threshold = 2;
    // list is full
    private const int ListSize = 4;
    // steps to relocate
    private const int Limit = 5;

    private long _setSize = 0;
    private volatile int _capacity;
    private volatile List<T>[,] _table;
    private readonly Mutex[,] _locks;

    public StripedCuckooHashSet(int capacity)
    {
        _capacity = capacity;

        _locks = new Mutex[2, capacity];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < capacity; j++)
            {
                _locks[i, j] = new Mutex();
            }
        }

        _table = new List<T>[2, capacity];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < capacity; j++)
            {
                _table[i, j] = new List<T>(ListSize);
            }
        }
    }

    public bool Contains(T x)
    {
        Acquire(x);
        try
        {
            List<T> set0 = _table[0, Hash0(x) % _capacity];
            if (set0.Contains(x))
            {
                return true;
            }
            else
            {
                List<T> set1 = _table[1, Hash1(x) % _capacity];
                if (set1.Contains(x))
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

    public bool Add(T x)
    {
        Acquire(x);
        int h0 = Hash0(x) % _capacity, h1 = Hash1(x) % _capacity;
        int i = -1, h = -1;
        bool mustResize = false;
        try
        {
            if (Contains(x)) return false;
            List<T> set0 = _table[0, h0];
            List<T> set1 = _table[1, h1];
            if (set0.Count < Threshold)
            {
                set0.Add(x);
                Interlocked.Increment(ref _setSize);
                return true;
            }
            else if (set1.Count < Threshold)
            {
                set1.Add(x);
                Interlocked.Increment(ref _setSize);
                return true;
            }
            else if (set0.Count < ListSize)
            {
                set0.Add(x);
                Interlocked.Increment(ref _setSize);
                i = 0; 
                h = h0;
            }
            else if (set1.Count < ListSize)
            {
                set1.Add(x);
                Interlocked.Increment(ref _setSize);
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
            Interlocked.Increment(ref _setSize);
        }
        else if (!Relocate(i, h))
        {
            Resize();
        }
        return true;
    }

    public bool Remove(T x)
    {
        Acquire(x);
        try
        {
            List<T> set0 = _table[0, Hash0(x) % _capacity];
            if (set0.Contains(x))
            {
                set0.Remove(x);
                Interlocked.Decrement(ref _setSize);
                return true;
            }
            else
            {
                List<T> set1 = _table[1, Hash1(x) % _capacity];
                if (set1.Contains(x))
                {
                    set1.Remove(x);
                    Interlocked.Decrement(ref _setSize);
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

    public int Count() => (int)Interlocked.Read(ref _setSize);

    private int Hash0(T i)
    {
        return i.Item1.GetHashCode();
    }

    private int Hash1(T i)
    {
        return i.Item2.GetHashCode();
    }

    private void Acquire(T x)
    {
        _locks[0, Hash0(x) % _locks.GetLength(1)].WaitOne();
        _locks[1, Hash1(x) % _locks.GetLength(1)].WaitOne();
    }

    private void Release(T x)
    {
        _locks[0, Hash0(x) % _locks.GetLength(1)].ReleaseMutex();
        _locks[1, Hash1(x) % _locks.GetLength(1)].ReleaseMutex();
    }

    private void Resize()
    {
        int oldCapacity = _capacity;
        for (int i = 0; i < _locks.GetLength(1); i++)
        {
            _locks[0, i].WaitOne();
        }

        try
        {
            if (_capacity != oldCapacity)
            {
                return;
            }

            Interlocked.Exchange(ref _setSize, 0);

            List<T>[,] oldTable = _table;
            _capacity = 2 * _capacity;
            _table = new List<T>[2, _capacity];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < _capacity; j++)
                {
                    _table[i, j] = new List<T>(ListSize);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < oldCapacity; j++)
                {
                    foreach (T z in oldTable[i, j])
                    {
                        Add(z);
                    }
                }
            }
        }
        finally
        {
            for (int i = 0; i < _locks.GetLength(1); i++)
            {
                _locks[0, i].ReleaseMutex();
            }
        }
    }

    private bool Relocate(int i, int hi)
    {
        int hj = 0;
        int j = 1 - i;
        for (int round = 0; round < Limit; round++)
        {
            List<T> iSet = _table[i, hi];
            T y = iSet[0];
            switch (i)
            {
                case 0:
                    hj = Hash1(y) % _capacity;
                    break;
                case 1:
                    hj = Hash0(y) % _capacity;
                    break;
            }
            Acquire(y);
            List<T> jSet = _table[j, hj];
            try
            {
                if (iSet.Remove(y))
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
                else if (iSet.Count < Threshold)
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