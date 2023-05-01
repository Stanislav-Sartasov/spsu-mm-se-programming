namespace ExamSystem.ConcurrentCollections;

public class CoarseGrainedHashSet<T>
{
    // TODO: RWLock

    private List<T>[] _table;
    private int _setSize;
    private readonly Mutex _lock = new();

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

    // TODO: remove unused arg
    private void Acquire(T x) => _lock.WaitOne();
    private void Release(T x) => _lock.ReleaseMutex();

    private void Resize()
    {
        int oldCapacity = _table.Length;
        _lock.WaitOne();
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
            _lock.ReleaseMutex();
        }
    }

    public bool Contains(T x)
    {
        Acquire(x);
        try
        {
            int myBucket = Math.Abs(x.GetHashCode() % _table.Length);
            return _table[myBucket].Contains(x);
        }
        finally
        {
            Release(x);
        }
    }
    public bool Add(T x)
    {
        bool result = false;
        Acquire(x);
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
            Release(x);
        }
        if (PolicyDemandsResize)
            Resize();
        return result;
    }
}