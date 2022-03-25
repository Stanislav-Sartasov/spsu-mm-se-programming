using System;
using System.Collections;
using System.Collections.Generic;

namespace DynamicArray
{
	public class DynamicArray<T> : IList<T>
	{
		private T[] _items;
		private int _maxArrayCapacity;

		public DynamicArray()
		{
			_maxArrayCapacity = 8;
			Count = 0;

			_items = new T[_maxArrayCapacity];
		}

		public int Count { get; private set; }

		public bool IsReadOnly => false;
		
		public void Add(T item)
		{
			Count++;
			if (Count > _maxArrayCapacity)
			{
				_maxArrayCapacity *= 2;
				var newItems = new T[_maxArrayCapacity];
				for (var i = 0; i < Count - 1; i++)
					newItems[i] = _items[i];
				_items = newItems;
			}

			_items[Count - 1] = item;
		}

		public void Clear()
		{
			Count = 0;
			_maxArrayCapacity = 8;

			_items = new T[_maxArrayCapacity];
		}

		public bool Contains(T item)
		{
			return IndexOf(item) != -1;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array.Length < arrayIndex + Count)
				throw new ArgumentException();

			for (var i = 0; i < Count; i++) array[i + arrayIndex] = _items[i];
		}

		public bool Remove(T item)
		{
			var index = IndexOf(item);

			if (index == -1)
				return false;

			RemoveAt(index);
			return true;
		}

		public int IndexOf(T item)
		{
			var index = 0;
			foreach (var current in this)
			{
				if (item.Equals(current))
					return index;
				index++;
			}

			return -1;
		}

		public void Insert(int index, T item)
		{
			if (index >= Count)
				throw new ArgumentOutOfRangeException();

			Add(_items[Count - 1]);
			for (var i = Count - 1; i > index; i--)
				_items[i] = _items[i - 1];

			this[index] = item;
			Count++;
		}

		public void RemoveAt(int index)
		{
			if (index > Count)
				throw new ArgumentOutOfRangeException();

			Count--;
			for (var i = index; i < Count; i++) _items[i] = _items[i + 1];
		}

		public T this[int index]
		{
			get
			{
				if (index < Count && index > -1)
					return _items[index];
				throw new ArgumentOutOfRangeException();
			}
			set
			{
				if (index < Count && index > -1)
					_items[index] = value;
				else
					throw new ArgumentOutOfRangeException();
			}
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return (IEnumerator<T>) GetEnumerator();
		}

		public IEnumerator GetEnumerator()
		{
			return new DynamicArrayEnumerator<T>(_items, Count);
		}
	}
}