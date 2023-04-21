using NUnit.Framework;
using HashTable;

namespace HashTableTests
{
    public class HashTableTests
    {
        HashTable<int> hashTable;

        [SetUp]
        public void Setup()
        {
            hashTable = new HashTable<int>();
        }

        [Test]
        public void AddFirstElementTest()
        {
            hashTable.Add(5);
            Assert.AreEqual(hashTable.Buckets[5].Head.Data, 5);
            Assert.AreEqual(hashTable.Count, 1);
        }

        [Test]
        public void AddSomeElementsTest()
        {
            for (int i = 0; i < 20; i++)
            {
                hashTable.Add(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(hashTable.Buckets[i].Head.Data, i);
                Assert.AreEqual(hashTable.Buckets[i + 10].Head.Data, i + 10);
            }
            Assert.AreEqual(hashTable.Count, 20);
        }

        [Test]
        public void AddEqualElementsTest()
        {
            for (int i = 0; i < 10; i++)
            {
                hashTable.Add(1);
            }

            Assert.AreEqual(hashTable.Buckets[1].Head.Data, 1);
            Assert.AreEqual(hashTable.Buckets[1].Head.Next, null);
            Assert.AreEqual(hashTable.Count, 1);
        }

        [Test]
        public void DeleteSingleElementTest()
        {
            hashTable.Add(5);
            hashTable.Delete(5);

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(hashTable.Buckets[i].Head, null);
            }
            Assert.AreEqual(hashTable.Count, 0);
        }

        [Test]
        public void DeleteSomeElementsTest()
        {
            hashTable.Add(5);
            hashTable.Add(15);
            hashTable.Add(1);
            hashTable.Add(2);
            hashTable.Delete(15);
            hashTable.Delete(15);
            hashTable.Delete(1);
            hashTable.Delete(2);

            Assert.AreEqual(hashTable.Buckets[5].Head.Data, 5);
            Assert.AreEqual(hashTable.Buckets[5].Head.Next, null);
            Assert.AreEqual(hashTable.Buckets[1].Head, null);
            Assert.AreEqual(hashTable.Buckets[2].Head, null);
            Assert.AreEqual(hashTable.Count, 1);
        }

        [Test]
        public void SearchTest()
        {
            for (int i = 0; i < 10; i++)
            {
                hashTable.Add(i * 2);
            }

            for (int i = 0; i < 10; i += 2)
            {
                Assert.IsTrue(hashTable.Search(i));
                hashTable.Delete(i);
                Assert.IsFalse(hashTable.Search(i));
            }
            Assert.IsTrue(hashTable.Search(18));
            Assert.IsFalse(hashTable.Search(17));
        }

        [Test]
        public void RebalanceTest()
        {
            hashTable = new HashTable<int>(10);
            hashTable.Add(5);
            hashTable.Add(15);
            hashTable.Add(25);
            hashTable.Add(35);

            Assert.AreEqual(hashTable.Count, 4);
            Assert.AreEqual(hashTable.Buckets.Length, 31);
        }

        [Test]
        public void CloneAccuracyTest()
        {
            hashTable.Add(3);
            var hashTableClone = (HashTable<int>) hashTable.Clone();

            for (int i = 0; i < 10; i++)
            {
                if (i == 3) continue; 
                Assert.AreEqual(hashTable.Buckets[i].Head, hashTableClone.Buckets[i].Head);
            }
            Assert.AreEqual(hashTable.Buckets[3].Head.Data, hashTableClone.Buckets[3].Head.Data);
            Assert.AreEqual(hashTable.Count, hashTableClone.Count);
        }

        [Test]
        public void DeepCopyTest()
        {
            hashTable.Add(8);
            var hashTableClone = (HashTable<int>)hashTable.Clone();
            hashTable.Delete(8);
            hashTableClone.Add(4);

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(null, hashTable.Buckets[i].Head);
                if (i == 4 || i == 8) continue;
                Assert.AreEqual(hashTable.Buckets[i].Head, hashTableClone.Buckets[i].Head);
            }
            Assert.AreEqual(4, hashTableClone.Buckets[4].Head.Data);
            Assert.AreEqual(8, hashTableClone.Buckets[8].Head.Data);
            Assert.AreEqual(2, hashTableClone.Count);
        }
    }
}