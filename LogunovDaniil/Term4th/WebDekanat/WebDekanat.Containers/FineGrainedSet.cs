namespace WebDekanat.Containers
{
	public class FineGrainedSet : IExamSystem
	{
		private Node<Entry> _head = new(long.MinValue);
		private Node<Entry> _tail = new(long.MaxValue);
		private int count;

		public FineGrainedSet()
		{
			_head.Next = _tail;
		}

		public void Add(long studentId, long courseId)
        {
            var newNode = new Node<Entry>(new Entry(studentId, courseId));
            var newKey = newNode.Key;

            var pred = _head;
            pred.Lock();
            try
            {
                var curr = pred.Next;
                curr.Lock();
                try
                {
                    while (curr.Key < newKey)
                    {
                        pred.Unlock();
                        pred = curr;
                        curr = curr.Next;
                        curr.Lock();
                    }

                    if (curr.Key == newKey)
                        return;

                    newNode.Next = curr;
                    pred.Next = newNode;
                    
                    Interlocked.Increment(ref count);
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

		public bool Contains(long studentId, long courseId)
        {
            var key = new Node<Entry>(new Entry(studentId, courseId)).GetHashCode();

            var pred = _head;
            pred.Lock();
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

		public void Remove(long studentId, long courseId)
        {
            var keyToRemove = new Node<Entry>(new Entry(studentId, courseId)).GetHashCode();

            var pred = _head;
            pred.Lock();
            try
            {
                var curr = pred.Next;
                curr.Lock();
                try
                {
                    while (curr.Key < keyToRemove)
                    {
                        pred.Unlock();
                        pred = curr;
                        curr = curr.Next;
                        curr.Lock();
                    }

                    if (curr.Key != keyToRemove)
                        return;

                    pred.Next = curr.Next;

                    Interlocked.Decrement(ref count);
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

		public int Count { get { return count; } }
	}
}
