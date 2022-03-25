using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
	public class DoublyList<T>
	{
		public Node<T>? Head;
		public Node<T>? Tail;
		public int Length;

		public DoublyList()
		{
			Head = null;
			Tail = null;
			Length = 0;
		}

		public void AddFirst(T data)
		{
			Node<T> currentNode = new Node<T>(data);

			if (Head == null)
			{
				Head = currentNode;
				Tail = currentNode;
			}
			else
			{
				currentNode.Next = Head;
				currentNode.Next.Previous = currentNode;
				currentNode.Previous = null;
				Head = currentNode;
			}

			Length++;
		}

		public void AddLast(T data)
		{
			Node<T> currentNode = new Node<T>(data);

			if (Head == null)
			{
				Head = currentNode;
				Tail = currentNode;
			}
			else
			{
				Tail.Next = currentNode;
				currentNode.Previous = Tail;
			}
			Tail = currentNode;
			Length++;
		}

		public void RemoveFirst()
		{
			if (Head == null)
			{
				return;
			}
			else
			{
				Head = Head.Next;
				Head.Previous = null;
			}

			Length--;
		}

		public void RemoveLast()
		{
			if (Head == null)
			{
				return;
			}
			else
			{
				Tail = Tail.Previous;
				Tail.Next = null;
			}

			Length--;
		}

		public void Remove(T data)
		{
			if (Head == null)
			{
				return;
			}
			else
			{
				Node<T> currentNode = Head;
				while (!Equals(currentNode.Data, data) && currentNode.Next != null)
				{
					currentNode = currentNode.Next;
				}
				if (Equals(currentNode.Data, data))
				{
					currentNode.Previous.Next = currentNode.Next;
					currentNode.Next.Previous = currentNode.Previous;
					currentNode.Next = null;
					currentNode.Previous = null;
				}
				else
				{
					return;
				}

				Length--;
			}
		}

		public void Clear()
		{
			Head = null;
			Tail = null;
			Length = 0;
		}

		public int Find(T data)
		{
			int index = 0;
			Node<T> currentNode = Head;
			while (!Equals(currentNode.Data, data) && currentNode.Next != null)
			{
				currentNode = currentNode.Next;
				index++;
			}

			if (!Equals(currentNode.Data, data))
			{
				return -1;
			}
			else
			{
				return index;
			}
		}

		public int FindLast(T data)
		{
			int index = 0;
			Node<T> currentNode = Tail;
			while (!Equals(currentNode.Data, data) && currentNode.Previous != null)
			{
				currentNode = currentNode.Previous;
				index++;
			}
			if (!Equals(currentNode.Data, data))
			{
				return -1;
			}
			else
			{
				return index;
			}
		}

		public T GetData(int index)
		{
			int count = 0;
			Node<T> currentNode = Head;
			if (index < 0 || index >= Length)
			{
				throw new IndexOutOfRangeException("This index does not exist in this list.");
			}
			while (index != count)
			{
				currentNode = currentNode.Next;
				count++;
			}
			return currentNode.Data;
		}

		public T GetDataLast(int index)
		{
			int count = 0;
			Node<T> currentNode = Tail;
			if (index < 0 || index >= Length)
			{
				throw new IndexOutOfRangeException("This index does not exist in this list.");
			}
			while (index != count)
			{
				currentNode = currentNode.Previous;
				count++;
			}
			return currentNode.Data;
		}
	}
}
