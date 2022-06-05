using System;

namespace HashTableLib
{
	public class Link<T>
	{
		public int Key;
		public T Value;

		public Link(int key, T value)
		{
			Key = key;
			Value = value;
		}
	}
}
