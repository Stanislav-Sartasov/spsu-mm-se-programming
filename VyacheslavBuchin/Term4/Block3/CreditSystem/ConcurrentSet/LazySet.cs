namespace ConcurrentSet;

public class LazySet<T> : ISet<T>
{
    private readonly Node<T> _tail = new(int.MaxValue);
    private readonly Node<T> _head = new(int.MinValue);
    private readonly ReaderWriterLockSlim _countLock = new();
    private int _count = 0;

    public LazySet()
    {
        _head.Next = _tail;
    }

    private bool Validate(Node<T> pred, Node<T> curr)
    {
        return !pred.Marked && !curr.Marked && pred.Next == curr;
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
        while (true)
        {
            var pred = _head;
            var curr = _head.Next;
            while (curr.Key < key)
            {
                pred = curr;
                curr = curr.Next;
            }

            pred.Lock();
            try
            {
                curr.Lock();
                try
                {
                    if (Validate(pred, curr))
                    {
                        if (curr.Key == key)
                        {
                            return false;
                        }
                        else
                        {
                            var node = new Node<T>(item) { Next = curr };
                            pred.Next = node;
                            _countLock.EnterWriteLock();
                            ++_count;
                            _countLock.ExitWriteLock();
                            return true;
                        }
                    }
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
    }

    public bool Remove(T item)
    {
        var key = item!.GetHashCode();
        while (true)
        {
            var pred = _head;
            var curr = _head.Next;
            while (curr.Key < key)
            {
                pred = curr;
                curr = curr.Next;
            }

            pred.Lock();
            try
            {
                curr.Lock();
                try
                {
                    if (Validate(pred, curr))
                    {
                        if (curr.Key != key)
                        {
                            return false;
                        }
                        else
                        {
                            curr.Marked = true;
                            pred.Next = curr.Next;
                            _countLock.EnterWriteLock();
                            --_count;
                            _countLock.ExitWriteLock();
                            return true;
                        }
                    }
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
    }

    public bool Contains(T item)
    {
        var key = item!.GetHashCode();
        var curr = _head;
        while (curr.Key < key)
            curr = curr.Next;
        return curr.Key == key && !curr.Marked;
    }
}
