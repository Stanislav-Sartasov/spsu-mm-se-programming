using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
	public class Node<T>
	{
		public T Data { get; set; }
		public Node<T>? Next;
		public Node<T>? Previous;

		public Node(T data)
		{
			Data = data;
			Next = null;
			Previous = null;
		}
	}
}
