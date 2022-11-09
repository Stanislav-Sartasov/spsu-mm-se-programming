using System;

namespace HashTable
{
	class Program
	{
		static void Main(string[] args)
		{
			MyHashTable<string, double> hashTable = new MyHashTable<string, double>();

			Console.WriteLine("This program implements a hash table and standard tools for working with it");
			hashTable.Add("key", 1.5);
			hashTable.Search("key");
			hashTable.Delete("key");
			hashTable.Search("key");
			hashTable.Output();
		}
	}
}
