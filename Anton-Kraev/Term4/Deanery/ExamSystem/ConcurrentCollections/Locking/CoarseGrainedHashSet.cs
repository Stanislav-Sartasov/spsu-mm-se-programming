namespace ExamSystem.ConcurrentCollections.Locking;

internal class CoarseGrainedHashSet<T>
{
    private List<T>[] _table;
    private int _setSize;
    private readonly ReaderWriterLockSlim _lock = new();

    public CoarseGrainedHashSet(int capacity)
    {
        _setSize = 0;
        _table = new List<T>[capacity];
        for (int i = 0; i < capacity; i++)
        {
            _table[i] = new List<T>();
        }
    }

    private bool PolicyDemandsResize => _setSize / _table.Length > 4;

    private void Resize()
    {
        int oldCapacity = _table.Length;
        _lock.EnterWriteLock();
        try
        {
            if (oldCapacity != _table.Length)
            {
                return; // someone beat us to it
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
            _lock.ExitWriteLock();
        }
    }

    public bool Contains(T x)
    {
        _lock.EnterReadLock();
        try
        {
            int myBucket = Math.Abs(x.GetHashCode() % _table.Length);
            return _table[myBucket].Contains(x);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
    public bool Add(T x)
    {
        bool result = false;
        _lock.EnterWriteLock();
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
            _lock.ExitWriteLock();
        }
        if (PolicyDemandsResize)
            Resize();
        return result;
    }

    public bool Remove(T x)
    {
        _lock.EnterWriteLock();
        try
        {
            int myBucket = Math.Abs(x.GetHashCode() % _table.Length);
            bool result = _table[myBucket].Remove(x);
            _setSize = result ? _setSize - 1 : _setSize;
            return result;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public int Count()
    {
        _lock.EnterReadLock();
        try
        {
            return _setSize;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
}