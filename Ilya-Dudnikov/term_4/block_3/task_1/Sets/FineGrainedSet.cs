namespace Sets;

public class FineGrainedSet<T> : ISet<T>
{
    private Node<T> head;
    private int count = 0;
    private ReaderWriterLockSlim rwLock = new();

    public FineGrainedSet()
    {
        head = new Node<T>(int.MinValue);
        head.Next = new Node<T>(int.MaxValue);
    }

    public bool Add(T item)
    {
        var key = item.GetHashCode();
        head.Lock();
        Node<T> pred = head;
        try
        {
            Node<T> curr = pred.Next;
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

                if (curr.Key == key) return false;
                Node<T> newNode = new(item);
                newNode.Next = curr;
                pred.Next = newNode;
                
                rwLock.EnterWriteLock();
                count++;
                rwLock.ExitWriteLock();
                
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
        Node<T> pred = null, curr = null;
        var key = item.GetHashCode();
        head.Lock();
        try
        {
            pred = head;
            curr = pred.Next;
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
                    pred.Next = curr.Next;
                    
                    rwLock.EnterWriteLock();
                    count--;
                    rwLock.ExitWriteLock();
                    
                    return true;
                }

                return false;
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

    public bool Contains(T item)
    {
        Node<T> pred = null, curr = null;
        var key = item.GetHashCode();
        head.Lock();
        try
        {
            pred = head;
            curr = pred.Next;
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
            pred.Unlock();
        }
    }

    public int Count()
    {
        rwLock.EnterReadLock();
        var result = count;
        rwLock.ExitReadLock();
        return result;
    }
}