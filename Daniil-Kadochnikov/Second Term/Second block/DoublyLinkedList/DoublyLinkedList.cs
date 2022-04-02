using System;

namespace DoublyLinkedList
{
	public class DoublyLinkedList<T>
	{
		public DoublyLinkedNode<T> First;
		public DoublyLinkedNode<T> Last;
		
		public DoublyLinkedList()
		{
			First = null;
			Last = null;
		}
		
		public int? Add(T data)
		{
			DoublyLinkedNode<T> node = new DoublyLinkedNode<T>(data);

			if (First != null)
			{
				Last.Next = node;
				node.Prev = Last;
				Last = node;
				return 1;
			}
			else
			{
				First = node;
				Last = node;
				return 1;
			}
		}

		public DoublyLinkedNode<T> Find(T data)
		{
			DoublyLinkedNode<T> node = First;
			while (node != null)
			{
				if (Equals(node.Data, data)) return node;
				node = node.Next;
			}
			return null;
		}

		public int? Remove(T data)
		{
			DoublyLinkedNode<T> node = Find(data);
			if (Equals(node, First))
			{
				First = node.Next;
				First.Prev = null;
				return 1;
			}
			else if (Equals(node, Last))
			{
				Last = node.Prev;
				Last.Next = null;
				return 1;
			}
			else if (!Equals(node, null))
			{
				node.Prev.Next = node.Next;
				node.Next.Prev = node.Prev;
				return 1;
			}
			return null;
		}
	}
}
