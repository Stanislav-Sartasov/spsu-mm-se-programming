using System.Collections;

namespace DynamicArray
{
	public class DynamicArrayEnumerator<T> : IEnumerator
	{
		private int _currentIndex;
		private readonly T[] _items;
		private readonly int _size;

		public DynamicArrayEnumerator(T[] items, int size)
		{
			_items = items;
			_size = size;
			_currentIndex = -1;
		}

		public object Current => _items[_currentIndex];

		public bool MoveNext()
		{
			_currentIndex++;
			return _currentIndex < _size;
		}

		public void Reset()
		{
			_currentIndex = -1;
		}

		object IEnumerator.Current => Current;
	}
}