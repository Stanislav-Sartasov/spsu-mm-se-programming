using System;
using System.Collections.Generic;

namespace Task_3
{
	public class DynamicArray<T>
	{
		private T[] Items;
		public int Count { get; private set; }

		public DynamicArray()
		{
			Items = new T[2];
			Count = 0;
		}

		public T this[int index]
		{
			get
			{
				if (!(index >= 0 && index < Count))
					throw new ArgumentOutOfRangeException();
				return Items[index];
			}
			set
			{
				if (!(index >= 0 && index < Count))
					throw new ArgumentOutOfRangeException();
				Items[index] = value;
			}
		}

		private void ResizeArray(int newCount)
		{
			T[] newArray = new T[newCount];
			for (int i = 0; i < Count; i++)
				newArray[i] = Items[i];

			Items = newArray;
		}

		public void Add(T item)
		{
			if (Count == Items.Length)
				ResizeArray(Count * 2);

			Items[Count] = item;
			Count++;
		}

		public void RemoveAt(int index)
		{
			if (!(index >= 0 && index < Count))
				throw new ArgumentOutOfRangeException();

			for (int i = index + 1; i < Count; i++)
				Items[i - 1] = Items[i];
			Count--;

			if (Count == Items.Length / 4 && Items.Length != 0)
				ResizeArray(Items.Length / 2);
		}

		public bool Remove(T item)
		{
			int index = IndexOf(item);
			if (index == -1)
				return false;

			RemoveAt(index);
			return true;
		}

		public void Clear()
		{
			Count = 0;
			Items = new T[2];
		}

		public bool Contains(T item)
		{
			for (int i = 0; i < Count; i++)
				if (Items[i].Equals(item))
					return true;

			return false;
		}

		public int IndexOf(T item)
		{
			for (int i = 0; i < Count; i++)
				if (Items[i].Equals(item))
					return i;

			return -1;
		}
	}
}