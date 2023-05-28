namespace ConcurrentSet;

public class FineGrainedSet<T> : ISet<T>
{
    private readonly Node<T> _head;
    private int _count = 0;
    private readonly ReaderWriterLockSlim _countLock = new();

    public FineGrainedSet()
    {
        _head = new Node<T>(int.MinValue)
        {
            Next = new Node<T>(int.MaxValue)
        };
    }

    public int Count()
    {
        _countLock.EnterReadLock();
        var count = _count;
        _countLock.ExitReadLock();
        return count;
    }

    public bool Add(T item)
    {
        var key = item!.GetHashCode();
        _head.Lock();
        var pred = _head;
        try
        {
            var curr = pred.Next;
            curr.Lock();
            try
            {
                while (curr.Key < key)
                {
                    pred.Unlock();
                    pred = curr;
                    curr = curr.Next;
                    curr.Lock();
                }

                if (curr.Key == key)
                {
                    return false;
                }

                var newNode = new Node<T>(item)
                {
                    Next = curr
                };
                pred.Next = newNode;
                _countLock.EnterWriteLock();
                ++_count;
                _countLock.ExitWriteLock();
                return true;
            }
            finally
            {
                curr.Unlock();
            }
        }
        finally
        {
            pred.Unlock();
        }
    }

    public bool Remove(T item)
    {
        Node<T>? pred = null;
        var key = item!.GetHashCode();
        _head.Lock();
        try
        {
            pred = _head;
            var curr = pred.Next;
            curr.Lock();
            try
            {
                while (curr.Key < key)
                {
                    pred.Unlock();
                    pred = curr;
                    curr = curr.Next;
                    curr.Lock();
                }

                if (curr.Key != key)
                {
                    return false;
                }

                pred.Next = curr.Next;
                _countLock.EnterWriteLock();
                --_count;
                _countLock.ExitWriteLock();
                return true;
            }
            finally
            {
                curr.Unlock();
            }
        }
        finally
        {
            pred?.Unlock();
        }
    }

    public bool Contains(T item)
    {
        Node<T>? pred = null;
        var key = item!.GetHashCode();
        _head.Lock();
        try
        {
            pred = _head;
            var curr = pred.Next;
            curr.Lock();
            try
            {
                while (curr.Key < key)
                {
                    pred.Unlock();
                    pred = curr;
                    curr = curr.Next;
                    curr.Lock();
                }

                return curr.Key == key;
            }
            finally
            {
                curr.Unlock();
            }
        }
        finally
        {
            pred?.Unlock();
        }
    }
}
