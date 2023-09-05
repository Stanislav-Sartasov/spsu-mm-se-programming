﻿namespace DoublyLinkedList
{
	public class DoublyLinkedList<T>
	{
		private DoublyNode<T>? _head;
		private DoublyNode<T>? _tail;

		public int Length { get; private set; }

		public void Add(T data)
		{
			DoublyNode<T> node = new DoublyNode<T>(data);

			if (_head is null)
				_head = node;
			else
			{
				_tail!.Next = node;
				node.Prev = _tail;
			}
            
			_tail = node;
            
			Length++;
		}

		public void Remove(int index)
		{
			DoublyNode<T> current = this.Get(index);

			// If last
			if (current.Next is null)
				_tail = current.Prev;
			else
				current.Next.Prev = current.Prev;
            
			// If first
			if (current.Prev is null)
				_head = current.Next;
			else
				current.Prev.Next = current.Next;

			Length--;
		}

		public int Find(T data)
		{
			DoublyNode<T>? current = _head;
			int index = 0;

			while (current is not null)
			{
				if (Equals(current.Data, data)) return index;
                
				index++;
				current = current.Next;
			}

			return -1;
		}

		public void Insert(int index, T data)
		{
			// Insert in the end
			if (index == Length)
			{
				this.Add(data);
				return;
			}
			
			DoublyNode<T> current = this.Get(index);
			DoublyNode<T> node = new DoublyNode<T>(data);

			// If before first
			if (current.Prev is null)
				_head = node;
			else
				current.Prev.Next = node;

			// Inserting node before current
			node.Prev = current.Prev;
			node.Next = current;
			current.Prev = node;

			Length++;
		}

		private DoublyNode<T> Get(int index)
		{
			// Checking the index
			// If the list is empty this will also work
			if (index < 0 || index >= Length)
				throw new IndexOutOfRangeException("Index was out of range");
			
			DoublyNode<T> current = _head!;

			for (int i = 0; i < index; i++)
				current = current.Next!;

			return current;
		}

		public T this[int index]
		{
			get => this.Get(index).Data;

			set => this.Get(index).Data = value;
		}
	}
}