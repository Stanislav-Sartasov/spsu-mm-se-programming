using System;

namespace DoublyLinkedList
{
	public class DoublyLinkedList<T>
	{
		private DoublyLinkedNode<T> first;
		private DoublyLinkedNode<T> last;
		public int Length;
		
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

		public int? FindIndex(T data)
		{
			var index = 1;
			DoublyLinkedNode<T> node = first;
			while (node != null)
			{
				if (Equals(node.data, data)) return index;
				node = node.next;
				index++;
			}
			Console.WriteLine("Data \"", data + "\" was nor found.");
			return null;
		}

		public T FindData(int index)
		{
			if (index > 0 && index < Length + 1)
			{
				DoublyLinkedNode<T> node = first;
				while (index > 1)
				{
					node = node.next;
					index--;
				}
				return node.data;
			}
			else
			{
				throw new IndexOutOfRangeException("Index  is out of the range.");
			}
		}

		public void Remove(int index)
		{
			Length--;
			if (index == 1)
			{
				first = first.next;
				first.prev = null;
			}
			else if (index == Length + 1)
			{
				last = last.prev;
				last.next = null;
			}
			else if (index > 1 && index < Length + 1)
			{
				DoublyLinkedNode<T> node = first.next;
				while (index > 2)
				{
					node = node.next;
					index--;
				}
				node.prev.next = node.next;
				node.next.prev = node.prev;
			}
			else
			{
				throw new IndexOutOfRangeException("Index  is out of the range.");
			}
		}
	}
}