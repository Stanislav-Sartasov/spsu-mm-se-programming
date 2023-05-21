namespace ExamSystem.ConcurrentCollections;

internal class StripedHashSet<T>
{
    private List<T>[] _table;
    private int _setSize;
    private readonly ReaderWriterLockSlim[] _locks;

    public StripedHashSet(int capacity)
    {
        _setSize = 0;
        _table = new List<T>[capacity];
        _locks = new ReaderWriterLockSlim[capacity];
        for (int i = 0; i < capacity; i++)
        {
            _table[i] = new List<T>();
            _locks[i] = new ReaderWriterLockSlim();
        }
    }

    private bool PolicyDemandsResize => _setSize / _table.Length > 4;

    private void Resize()
    {
        int oldCapacity = _table.Length;

        foreach (var l in _locks)
        {
            l.EnterWriteLock();
        }

        try
        {
            if (oldCapacity != _table.Length)
            {
                return;
            }
            int newCapacity = 2 * oldCapacity;
            List<T>[] oldTable = _table;
            _table = new List<T>[newCapacity];
            for (int i = 0; i < newCapacity; i++)
                _table[i] = new List<T>();
            foreach (List<T> bucket in oldTable)
            {
                foreach (T x in bucket)
                {
                    _table[x.GetHashCode() % _table.Length].Add(x);
                }
            }
        }
        finally
        {
            foreach (var l in _locks)
            {
                l.ExitWriteLock();
            }
        }
    }

    public bool Contains(T x)
    {
        _locks[Math.Abs(x.GetHashCode() % _locks.Length)].EnterReadLock();
        try
        {
            int myBucket = Math.Abs(x.GetHashCode() % _table.Length);
            return _table[myBucket].Contains(x);
        }
        finally
        {
            _locks[Math.Abs(x.GetHashCode() % _locks.Length)].ExitReadLock();
        }
    }

    public bool Add(T x)
    {
        bool result = false;
        _locks[Math.Abs(x.GetHashCode() % _locks.Length)].EnterWriteLock();
        try
        {
            int myBucket = Math.Abs(x.GetHashCode() % _table.Length);
            if (!_table[myBucket].Contains(x))
            {
                _table[myBucket].Add(x);
                result = true;
                _setSize++;
            }
        }
        finally
        {
            _locks[Math.Abs(x.GetHashCode() % _locks.Length)].ExitWriteLock();
        }
        if (PolicyDemandsResize)
            Resize();
        return result;
    }

    public bool Remove(T x)
    {
        _locks[Math.Abs(x.GetHashCode() % _locks.Length)].EnterWriteLock();
        try
        {
            int myBucket = Math.Abs(x.GetHashCode() % _table.Length);
            bool result = _table[myBucket].Remove(x);
            _setSize = result ? _setSize - 1 : _setSize;
            return result;
        }
        finally
        {
            _locks[Math.Abs(x.GetHashCode() % _locks.Length)].ExitWriteLock();
        }
    }

    public int Count() => _setSize;
}