using Microsoft.VisualStudio.TestTools.UnitTesting;
using HashTable;

namespace HashTableTest
{
	[TestClass]
	public class HashTableTest
	{
		[TestMethod]
		public void CreateTest()
		{
			MyHashTable<string, double> testHashTable = new MyHashTable<string, double>();
			Assert.IsNotNull(testHashTable);
		}

		[TestMethod]
		public void AddDeleteMethodTest()
		{
			MyHashTable<string, double> testHashTable = new MyHashTable<string, double>();
			testHashTable.Add("someKey", 1.9);
			double expected = 1.9;
			int index = testHashTable.GetHashCode("someKey");
			Assert.AreEqual(expected, testHashTable.hashTable[index].Value);
			testHashTable.Delete("someKey");
			Assert.AreEqual(true, testHashTable.hashTable[index].Remote);
		}
		[TestMethod]
		public void BalancingMethodTest()
		{
			int expectedSize = 16;
			MyHashTable<string, int> testHashTable = new();
			for (int i = 0; i < 7; i++)
			{
				testHashTable.Add($"{i}", i);
			}
			Assert.AreEqual(expectedSize, testHashTable.size);
		}
	}
}
