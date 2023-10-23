namespace WebDekanat.Containers
{
	public class LazySet : IExamSystem
	{
		private Node<Entry> _head = new(long.MinValue);
		private Node<Entry> _tail = new(long.MaxValue);
		private volatile int count = 0;

		public LazySet()
		{
			_head.Next = _tail;
		}

		private bool Validate(Node<Entry> pred, Node<Entry> curr)
		{
			return !pred.Marked && !curr.Marked && pred.Next == curr;
		}

		public void Add(long studentId, long courseId)
		{
			var newNode = new Node<Entry>(new Entry(studentId, courseId));
			var newKey = newNode.Key;
			while (true)
			{
				var pred = _head;
				var curr = _head.Next;

				while (curr.Key < newKey)
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
							if (curr.Key == newKey)
								return;
							else
							{
								pred.Next = newNode;
								newNode.Next = curr;
								Interlocked.Increment(ref count);
								return;
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

		public bool Contains(long studentId, long courseId)
		{
			var key = new Entry(studentId, courseId).GetHashCode();
			var curr = _head;
			while (curr.Key < key)
				curr = curr.Next;
			return curr.Key == key && !curr.Marked;
		}

		public void Remove(long studentId, long courseId)
		{
			var keyToRemove = new Entry(studentId, courseId).GetHashCode();
			while (true)
			{
				var pred = _head;
				var curr = _head.Next;

				while (curr.Key < keyToRemove)
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
							if (curr.Key == keyToRemove)
							{
								curr.Marked = true;
								pred.Next = curr.Next;
								Interlocked.Decrement(ref count);
								return;
							}
							else
								return;
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

		public int Count
		{
			get
			{
				return count;
			}
		}
	}
}
