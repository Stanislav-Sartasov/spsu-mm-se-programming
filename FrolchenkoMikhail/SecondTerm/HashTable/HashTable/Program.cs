namespace HashTable
{
	class Program
	{
		static void Main(string[] args)
		{
			MyHashTable<string, double> hashTable = new MyHashTable<string, double>();

			hashTable.Add("key", 1.5);
			hashTable.Search("key");
			hashTable.Delete("key");
			hashTable.Search("key");
			hashTable.Output();
		}
	}
}
