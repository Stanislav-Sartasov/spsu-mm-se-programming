using System;

namespace DoublyLinkedList
{
	public class DoublyLinkedList<T>
	{
		private DoublyLinkedNode<T> first;
		private DoublyLinkedNode<T> last;
		public int Length { get; private set; }

		public DoublyLinkedList()
		{
			first = null;
			last = null;
			Length = 0;
		}

		public void Add(T data)
		{
			Length++;
			DoublyLinkedNode<T> node = new DoublyLinkedNode<T>(data);

			if (first != null)
			{
				last.next = node;
				node.prev = last;
				last = node;
			}
			else
			{
				first = node;
				last = node;
			}
		}

		public int FindIndex(T data)
		{
			var index = 0;
			DoublyLinkedNode<T> node = first;
			while (node != null)
			{
				if (Equals(node.data, data)) return index;
				node = node.next;
				index++;
			}
			return -1;
		}

		public T FindData(int index)
		{
			if (index >= 0 && index < Length)
			{
				DoublyLinkedNode<T> node = first;
				while (index > 0)
				{
					node = node.next;
					index--;
				}
				return node.data;
			}
			throw new IndexOutOfRangeException("Index  is out of the range.");
		}

		public void Remove(int index)
		{
			if (index < 0 || index >= Length)
				throw new IndexOutOfRangeException("Index  is out of the range.");

			if (index == 0)
			{
				first = first.next;
				first.prev = null;
			}
			else if (index == Length - 1)
			{
				last = last.prev;
				last.next = null;
			}
			else
			{
				DoublyLinkedNode<T> node = first;
				while (index > 0)
				{
					node = node.next;
					index--;
				}
				node.prev.next = node.next;
				node.next.prev = node.prev;
			}

			Length--;
		}
	}
}