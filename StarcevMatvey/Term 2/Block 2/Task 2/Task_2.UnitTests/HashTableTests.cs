using NUnit.Framework;
using LibraryGeneric;

namespace Task_2.UnitTests
{
    public class HashTableTests
    {
        [Test]
        public void GetTest()
        {
            HashTable<int>  HashTable = new HashTable<int>();
            HashTable.Add(1, 2);
            Assert.AreEqual(HashTable.Get(1), 2);
            Assert.AreEqual(HashTable.Get(2), default(int));
            Assert.Pass();
        }

        [Test]
        public void AddTest()
        {
            HashTable<int> HashTable = new HashTable<int>();
            HashTable.Add(1, 2);
            Assert.AreEqual(HashTable.Size, 1);
            Assert.Pass();
        }

        [Test]
        public void RebalanceTest()
        {
            HashTable<int> HashTable = new HashTable<int>();
            HashTable.Add(1, 2);
            HashTable.Add(3, 3);
            Assert.AreEqual(HashTable.Divisior, 4);
            Assert.Pass();
        }

        [Test]
        public void RemoveTest()
        {
            HashTable<int> HashTable = new HashTable<int>();
            HashTable.Add(1, 2);
            HashTable.Remove(1);
            Assert.AreEqual(HashTable.Get(1), default(int));
            Assert.AreEqual(HashTable.Size, 0);
            Assert.Pass();
        }
    }
}