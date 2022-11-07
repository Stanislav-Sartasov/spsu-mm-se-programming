using System;

namespace HashTable
{
	public class MyHashTable<TKey, TValue>
	{
		public Cell<TKey, TValue>[] hashTable;
		public int size;
		public int step;
		public int full;
		
		public MyHashTable()
		{
			size = 8;
			step = 5;
			hashTable = new Cell<TKey, TValue>[size];
			full = 0;
		}

		public int GetHashCode(TKey key)
		{
			int hashCode = Math.Abs(key.GetHashCode()) % size;
			return hashCode;
		}

		public void Add(TKey key, TValue value)
		{
			Balancing();
			full++;
			int index = GetHashCode(key);
			while (hashTable[index] == null || !hashTable[index].Remote)
			{
				if (hashTable[index] == null)
				{
					hashTable[index] = new Cell<TKey, TValue>(key, value)
					{
						Remote = false
					};
					return;
				}
				index = (index + step) % size;
			}
			hashTable[index] = new Cell<TKey, TValue>(key, value)
			{
				Remote = false
			};
		}

		public void Delete (TKey key)
		{
			int index = GetHashCode(key);
			while (hashTable[index] == null || !hashTable[index].Key.Equals(key))
			{
				if (hashTable[index] == null)
				{
					Console.WriteLine("Value not found\n");
					return;
				}
				index = (index + step) % size;
			}
			hashTable[index].Remote = true;
			full--;
			Console.WriteLine($"Value {hashTable[index].Value} deleted\n");
		}

		public void Search (TKey key)
		{
			int index = GetHashCode(key);
			while (hashTable[index] == null || !hashTable[index].Key.Equals(key))
			{
				if (hashTable[index] == null)
				{
					Console.WriteLine("Value not found\n");
					return;
				}
				index = (index + step) % size;
			}
			if (hashTable[index].Remote)
			{
				Console.WriteLine("The value has been removed");
				return;
			}
			Console.WriteLine($"Found value: {hashTable[index].Value}");
		}

		public void Balancing()
		{
			if ((full + 1) * 4 > size * 3)
			{
				Cell<TKey, TValue>[] tempHashTable = new Cell<TKey, TValue>[size];
				Array.Copy(hashTable, tempHashTable, size);
				size *= 2;
				hashTable = new Cell<TKey, TValue>[size];
				full = 0;
				for (int i = 0; i < size/2; i++)
				{
					if (tempHashTable[i] == null)
						continue;
					else
					{
						Add(tempHashTable[i].Key, tempHashTable[i].Value);
					}
				}
			}
		}

		public void Output()
		{
			int count = 0;
			foreach (var cell in hashTable)
			{
				if (cell == null || cell.Remote)
					continue;
				else
				{
					Console.WriteLine($"key: {cell.Key} value: {cell.Value}");
					count++;
				}
			}
			if (count == 0)
			{
				Console.WriteLine("Table is empty");
			}
		}
	}
}
