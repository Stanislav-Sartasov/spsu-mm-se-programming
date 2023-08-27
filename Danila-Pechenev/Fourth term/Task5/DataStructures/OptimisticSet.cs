namespace DataStructures;

public class OptimisticSet<T> : ISet<T>
{
    private readonly Node<T> _head = new(int.MinValue);  // The head element is the minimum value of the key

    // There will always be two technical nodes in our list (in order not to handle edge cases):
    private readonly Node<T> _tail = new(int.MaxValue);  // Tail element is the maximum value of the key
    private volatile int count;

    public OptimisticSet()
    {
        _head.Next = _tail;
    }

    public bool Add(T item)
    {
        var key = item.GetHashCode();  // Defining the key of the element being added
        while (true)
        {
            var pred = _head;
            var curr = _head.Next;
            while
                (curr.Key < key)  // We scroll through the list until the key value of the current node
                                  // is less than the key of the one being added
            {
                pred = curr;
                curr = curr.Next;
            }

            pred.Lock.Lock();  // We block pred only at the moment when we have reached the nodes we are interested in
            try
            {
                curr.Lock.Lock();  // We block curr only at the moment when we have reached the nodes we are interested in
                try
                {
                    // Didn't something have time to change before our nodes were blocked?
                    if (Validate(pred, curr))
                    {
                        // Nothing has changed
                        if (curr.Key == key)
                        {
                            return false;  // List without repeated elements
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
                    pred.Lock.Unlock();
                }
            }
            finally
            {
                curr.Lock.Unlock();
            }
        }
    }

    public bool Remove(T item)
    {
        var key = item.GetHashCode();  // Defining the key of the element to be deleted
        while (true)
        {
            var pred = _head;
            var curr = _head.Next;
            while (curr.Key < key)  // We scroll through the list until the value of the key of the current node
                                    // is less than the key of the one being deleted
            {
                pred = curr;
                curr = curr.Next;
            }

            pred.Lock.Lock();  // We block pred only at the moment when we have reached the nodes we are interested in
            curr.Lock.Lock();  // We block curr only at the moment when we have reached the nodes we are interested in
            try
            {
                // Didn't something have time to change before our nodes were blocked?
                if (Validate(pred, curr))
                {
                    // Nothing has changed
                    if (curr.Key != key)
                    {
                        return false;  // List without repeated elements
                    }
                    else
                    {
                        pred.Next = curr.Next;
                        Interlocked.Decrement(ref count);
                        return true;
                    }
                }
            }
            finally
            {
                curr.Lock.Unlock();
                pred.Lock.Unlock();
            }
        }
    }


    public bool Contains(T item)
    {
        var key = item.GetHashCode();  // Defining the element key
        while (true)
        {
            var pred = _head;
            var curr = _head.Next;
            while (curr.Key < key)  // We scroll through the list until the key value of the current node
                                    // is less than the key of the one we are looking for
            {
                pred = curr;
                curr = curr.Next;
            }

            pred.Lock.Lock();  // We block pred only at the moment when we have reached the nodes we are interested in
            try
            {
                curr.Lock.Lock();  // We block curr only at the moment when we have reached the nodes we are interested in
                try
                {
                    // Didn't something have time to change before our nodes were blocked?
                    if (Validate(pred, curr))
                        // Nothing has changed
                        return curr.Key == key;
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

    private bool Validate(Node<T> pred, Node<T> curr)
    {
        var node = _head;
        while (node.Key <= pred.Key)
        {
            if (node == pred)
                // The pred node is what it was
                return pred.Next == curr;  // The curr node is what it was
            node = node.Next;
        }

        return false;  // Something has changed...
    }
}