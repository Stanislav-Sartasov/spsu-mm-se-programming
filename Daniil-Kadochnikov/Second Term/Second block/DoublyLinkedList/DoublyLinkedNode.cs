using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
	public class DoublyLinkedNode<T>
	{
		public DoublyLinkedNode<T> Prev;
		public T Data;
		public DoublyLinkedNode<T> Next;

		public DoublyLinkedNode(T data)
		{
			Prev = null;
			Data = data;
			Next = null;
		}
	}
}
