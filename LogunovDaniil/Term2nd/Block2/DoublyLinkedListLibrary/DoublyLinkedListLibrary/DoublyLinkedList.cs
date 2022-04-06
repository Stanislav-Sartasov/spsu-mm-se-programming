namespace DoublyLinkedListLibrary
{
	public class DoublyLinkedList<T>
	{
		private Node<T>? head = null;
		private Node<T>? tail = null;

		public int Count { get; private set; } = 0;

		public void Add(T value)
		{
			Node<T> newNode = new(value);
			if (tail == null)
				tail = head = newNode;
			else
			{
				tail.Next = newNode;
				newNode.Previous = tail;
				tail = newNode;
			}
			Count++;
		}

		// returns Count if the element wasn't found
		public int GetIndex(T value)
		{
			Node<T>? cur = head;
			int index = 0;
			while (cur != null && !cur.Data.Equals(value))
			{
				cur = cur.Next;
				index++;
			}
			return index;
		}

		// returns -1 if the element wasn't found
		public int GetIndexLast(T value)
		{
			Node<T>? cur = tail;
			int index = Count - 1;
			while (cur != null && !cur.Data.Equals(value))
			{
				cur = cur.Previous;
				index--;
			}
			return index;
		}

		// leaves the list unaffected if index is incorrect
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= Count)
				return;
			Count--;
			if (index == 0) // removing head
			{
				head = head.Next;
				if (head != null)
					head.Previous = null;
			}
			if (index == Count) // removing tail
			{
				tail = tail.Previous;
				if (tail != null)
					tail.Next = null;
			}
			if (index == 0 || index == Count)
				return;
			Node<T>? cur = head;
			for (int i = 0; i < index && cur != null; i++)
			{
				cur = cur.Next;
			}
			if (cur == null)
				return;
			cur.Previous.Next = cur.Next;
			cur.Next.Previous = cur.Previous;
		}

		public void Remove(T value)
		{
			RemoveAt(GetIndex(value));
		}

		public void RemoveLast(T value)
		{
			RemoveAt(GetIndexLast(value));
		}

		public T? GetAt(int index)
		{
			if (index < 0 || index >= Count)
				return default(T?);
			Node<T>? cur = head;
			for (int i = 0; i < index && cur != null; i++)
			{
				cur = cur.Next;
			}
			if (cur == null)
				return default(T?);
			return cur.Data;
		}
	}
}