using System;

namespace DoublyLinkedList
{
	public class DoublyLinkedList<T>
	{
		private DoublyLinkedNode<T> First;
		private DoublyLinkedNode<T> Last;
		public int Length;
		
		public DoublyLinkedList()
		{
			First = null;
			Last = null;
			Length = 0;
		}
		
		public void Add(T data)
		{
			Length++;
			DoublyLinkedNode<T> node = new DoublyLinkedNode<T>(data);

			if (First != null)
			{
				Last.Next = node;
				node.Prev = Last;
				Last = node;
			}
			else
			{
				First = node;
				Last = node;
			}
		}

		public int? FindIndex(T data)
		{
			var index = 1;
			DoublyLinkedNode<T> node = First;
			while (node != null)
			{
				if (Equals(node.Data, data)) return index;
				node = node.Next;
				index++;
			}
			Console.WriteLine("Data \"", data + "\" was nor found.");
			return null;
		}

		public T FindData(int index)
		{
			if (index > 0 && index < Length + 1)
			{
				DoublyLinkedNode<T> node = First;
				while (index > 1)
				{
					node = node.Next;
					index--;
				}
				return node.Data;
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
				First = First.Next;
				First.Prev = null;
			}
			else if (index == Length + 1)
			{
				Last = Last.Prev;
				Last.Next = null;
			}
			else if (index > 1 && index < Length + 1)
			{
				DoublyLinkedNode<T> node = First.Next;
				while (index > 2)
				{
					node = node.Next;
					index--;
				}
				node.Prev.Next = node.Next;
				node.Next.Prev = node.Prev;
			}
			else
			{
				throw new IndexOutOfRangeException("Index  is out of the range.");
			}
		}
	}
}