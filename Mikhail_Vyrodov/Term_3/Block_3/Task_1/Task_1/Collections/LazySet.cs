using Task_1;

namespace Task_1.Collections
{
    using InputType = Tuple<long, long>;

    public class LazySet : IExamSystem
    {
        private Node<InputType> _tail = new Node<InputType>(int.MaxValue);
        private Node<InputType> _head = new Node<InputType>(int.MinValue);

        public int Count { get; private set; }

        public LazySet()
        {
            _head.Next = _tail;
            Count = 0;
        }

        private bool Validate(Node<InputType> pred, Node<InputType> curr)
        {
            return !pred.Marked && !curr.Marked && pred.Next == curr;
        }

        public void Add(long studentId, long courseId)
        {
            AddInternal(studentId, courseId);
        }

        private bool AddInternal(long studentId, long courseId)
        {
            InputType item = new InputType(studentId, courseId);

            int key = item.GetHashCode();
            while (true)
            {
                Node<InputType> pred = _head;
                Node<InputType> curr = _head.Next;
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
                                Node<InputType> node = new Node<InputType>(item) { Next = curr };
                                pred.Next = node;
                                Count++;
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

        public void Remove(long studentId, long courseId)
        {
            RemoveInternal(studentId, courseId);
        }

        private bool RemoveInternal(long studentId, long courseId)
        {
            InputType item = new InputType(studentId, courseId);

            int key = item.GetHashCode();
            while (true)
            {
                Node<InputType> pred = _head;
                Node<InputType> curr = _head.Next;
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
                                Count--;
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

        public bool Contains(long studentId, long courseId)
        {
            InputType item = new InputType(studentId, courseId);

            int key = item.GetHashCode();
            Node<Tuple<long, long>> curr = _head;
            while (curr.Key < key)
                curr = curr.Next;
            return curr.Key == key && !curr.Marked;
        }
    }
}