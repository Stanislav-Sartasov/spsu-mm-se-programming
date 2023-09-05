namespace LocksContinued
{
    public class LazySet<T>
    {
        private Node<T> _tail = new Node<T>(int.MaxValue);
        private Node<T> _head = new Node<T>(int.MinValue);

        public LazySet()
        {
            _head.Next = _tail;
        }

        private bool Validate(Node<T> pred, Node<T> curr)
        {
            return !pred.Marked && !curr.Marked && pred.Next == curr;
        }

        public bool Add(T item)
        {
            int key = item.GetHashCode();
            while (true)
            {
                Node<T> pred = _head;
                Node<T> curr = _head.Next;
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
                                Node<T> node = new Node<T>(item) { Next = curr };
                                pred.Next = node;
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
            int key = item.GetHashCode();
            while (true)
            {
                Node<T> pred = _head;
                Node<T> curr = _head.Next;
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
                                curr.Marked = true;
                                pred.Next = curr.Next;
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


        public bool Сontains(T item)
        {
            int key = item.GetHashCode();
            Node<T> curr = _head;
            while (curr.Key < key)
                curr = curr.Next;
            return curr.Key == key && !curr.Marked;
        }
    }
}