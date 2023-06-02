namespace Sets;

internal class OptimisticSet<T> : ISet<T>
{
    private Node<T> _tail = new(int.MaxValue);
    private Node<T> _head = new(int.MinValue);
    private int count = 0;
    private ReaderWriterLockSlim rwLock = new();

    public OptimisticSet()
    {
        _head.Next = _tail;
    }

    private bool Validate(Node<T> pred, Node<T> curr)
    {
        Node<T> node = _head;
        while (node.Key <= pred.Key)
        {
            if (node == pred)
                return pred.Next == curr;
            node = node.Next;
        }

        return false;
    }

    public bool Add(T item)
    {
        var key = item.GetHashCode();
        while (true)
        {
            Node<T> pred = _head;
            Node<T> curr = _head.Next;
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
                            Node<T> node = new(item) { Next = curr };
                            pred.Next = node;
                            
                            rwLock.EnterWriteLock();
                            count++;
                            rwLock.ExitWriteLock();
                            
                            return true;
                        }
                    }
                }
                finally
                {
                    pred.Unlock();
                }
            }
            finally
            {
                curr.Unlock();
            }
        }
    }

    public bool Remove(T item)
    {
        var key = item.GetHashCode();
        while (true)
        {
            Node<T> pred = _head;
            Node<T> curr = _head.Next;
            while (curr.Key < key)
            {
                pred = curr;
                curr = curr.Next;
            }

            pred.Lock();
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
                        pred.Next = curr.Next;
                        
                        rwLock.EnterWriteLock();
                        count--;
                        rwLock.ExitWriteLock();
                        
                        return true;
                    }
                }
            }
            finally
            {
                curr.Unlock();
                pred.Unlock();
            }
        }
    }
    

    public int Count()
    {
        rwLock.EnterReadLock();
        var result = count;
        rwLock.ExitReadLock();
        return result;
    }


    public bool Contains(T item)
    {
        var key = item.GetHashCode();
        while (true)
        {
            Node<T> pred = _head;
            Node<T> curr = _head.Next;
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
                    if (Validate(pred, curr)) return curr.Key == key;
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
}