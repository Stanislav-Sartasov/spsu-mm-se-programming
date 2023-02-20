namespace LocksContinued
{
    class OptimisticSet<T>
    {
        private Node<T> _tail = new Node<T>(int.MaxValue);
        private Node<T> _head = new Node<T>(int.MinValue);

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
                            pred.Next = curr.Next;
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


        public bool Сontains(T item)
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
                            return curr.Key == key;
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
    }
}
