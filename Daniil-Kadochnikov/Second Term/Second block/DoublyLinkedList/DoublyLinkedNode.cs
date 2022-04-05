using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
	internal class DoublyLinkedNode<T>
	{
		internal DoublyLinkedNode<T> Prev;
		internal T Data;
		internal DoublyLinkedNode<T> Next;

		internal DoublyLinkedNode(T data)
		{
			Prev = null;
			Data = data;
			Next = null;
		}
	}
}