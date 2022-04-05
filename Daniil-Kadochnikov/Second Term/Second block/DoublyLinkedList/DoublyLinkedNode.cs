using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
	internal class DoublyLinkedNode<T>
	{
		internal DoublyLinkedNode<T> prev;
		internal T data;
		internal DoublyLinkedNode<T> next;

		internal DoublyLinkedNode(T data)
		{
			prev = null;
			this.data = data;
			next = null;
		}
	}
}