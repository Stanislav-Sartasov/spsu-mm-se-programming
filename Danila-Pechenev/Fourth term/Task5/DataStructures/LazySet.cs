namespace DataStructures;

public class LazySet<T> : ISet<T>
{
    private readonly Node<T> _head = new(int.MinValue);
    private readonly Node<T> _tail = new(int.MaxValue);
    private volatile int count;

    public LazySet()
    {
        _head.Next = _tail;
    }

    public bool Add(T item)
    {
        var key = item.GetHashCode();
        while (true)
        {
            var pred = _head;
            var curr = _head.Next;
            while (curr.Key < key)
            {
                pred = curr;
                curr = curr.Next;
            }

            pred.Lock.Lock();
            try
            {
                curr.Lock.Lock();
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
                            Interlocked.Increment(ref count);
                            return true;
                        }
                    }
                }
                finally
                {
                    curr.Lock.Unlock();
                }
            }
            finally
            {
                pred.Lock.Unlock();
            }
        }
    }

    public bool Remove(T item)
    {
        var key = item.GetHashCode();
        while (true)
        {
            var pred = _head;
            var curr = _head.Next;
            while (curr.Key < key)
            {
                pred = curr;
                curr = curr.Next;
            }

            pred.Lock.Lock();
            try
            {
                curr.Lock.Lock();
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
                            curr.Marked = true;  // Logical removal
                            pred.Next = curr.Next;  // Physical removal
                            Interlocked.Decrement(ref count);
                            return true;
                        }
                    }
                }
                finally
                {
                    curr.Lock.Unlock();
                }
            }
            finally
            {
                pred.Lock.Unlock();
            }
        }
    }

    public int Count()
    {
        return count;
    }


    public bool Contains(T item)
    {
        var key = item.GetHashCode();
        var curr = _head;
        while (curr.Key < key)
            curr = curr.Next;
        return curr.Key == key && !curr.Marked;  // If an element is not deleted logically, then it is not deleted physically
    }

    private bool Validate(Node<T> pred, Node<T> curr)
    {
        // Three conditions must be met:
        //     1) pred is not logically removed (and hence physically)
        //     2) curr is not logically removed (and hence physically)
        //     3) curr still follows pred
        return !pred.Marked && !curr.Marked && pred.Next == curr;
    }
}