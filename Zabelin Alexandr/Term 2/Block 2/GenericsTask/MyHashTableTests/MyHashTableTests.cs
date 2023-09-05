using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHashTable;

namespace MyHashTableTests
{
    [TestClass]
    public class MyHashTableTests
    {
        [TestMethod]
        public void IndexatorsValidTest()
        {
            MyHashTable<string, int> ht = new MyHashTable<string, int>(5);
            string key = "ABC";
            int expectedValue = -123;

            ht[key] = expectedValue;
            int actualValue = ht[key];

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void IndexatorsInvalidTest()
        {
            MyHashTable<float, int> ht = new MyHashTable<float, int>(1);
            float key = 3.7F;
            int expectedValue = default(int);

            int actualValue = ht[key];

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void IndexatorsNullAsKeyTest()
        {
            MyHashTable<string, bool> ht = new MyHashTable<string, bool>(1);
            bool expectedValue = default(bool);

            ht[null] = false;
            bool actualValue = ht[null];

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void IndexatorsNullAsValueTest()
        {
            MyHashTable<string, bool?> ht = new MyHashTable<string, bool?>(1);
            bool? expectedValue = null;

            ht["ABC"] = null;
            bool? actualValue = ht["ABC"];

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Add5BucketsTest()
        {
            MyHashTable<int, byte> ht = new MyHashTable<int, byte>(5);
            int key1 = -1, key2 = -2, key3 = -3, key4 = -4, key5 = -5;
            byte expected1 = 1, expected2 = 2, expected3 = 3, expected4 = 4, expected5 = 5;

            ht[key1] = expected1;
            ht[key2] = expected2;
            ht[key3] = expected3;
            ht[key4] = expected4;
            ht[key5] = expected5;

            Assert.AreEqual(expected1, ht[key1]);
            Assert.AreEqual(expected2, ht[key2]);
            Assert.AreEqual(expected3, ht[key3]);
            Assert.AreEqual(expected4, ht[key4]);
            Assert.AreEqual(expected5, ht[key5]);
        }

        [TestMethod]
        public void Add1BucketTest()
        {
            MyHashTable<int, int> ht = new MyHashTable<int, int>(1);
            int key1 = -1, key2 = -2, key3 = -3, key4 = -4, key5 = -5;
            int expected1 = 1, expected2 = 2, expected3 = 3, expected4 = 4, expected5 = 5;

            ht[key1] = expected1;
            ht[key2] = expected2;
            ht[key3] = expected3;
            ht[key4] = expected4;
            ht[key5] = expected5;

            Assert.AreEqual(expected1, ht[key1]);
            Assert.AreEqual(expected2, ht[key2]);
            Assert.AreEqual(expected3, ht[key3]);
            Assert.AreEqual(expected4, ht[key4]);
            Assert.AreEqual(expected5, ht[key5]);
        }

        [TestMethod]
        public void AddUpdateValuesTest()
        {
            MyHashTable<int, byte> ht = new MyHashTable<int, byte>(5);
            int key1 = -1, key2 = -2, key3 = -3, key4 = -4, key5 = -5;
            byte expected1 = 1, expected2 = 2, expected3 = 3, expected4 = 4, expected5 = 5;

            ht[key1] = 11;
            ht[key2] = 12;
            ht[key3] = 13;
            ht[key4] = 14;
            ht[key5] = 15;

            ht[key1] = expected1;
            ht[key2] = expected2;
            ht[key3] = expected3;
            ht[key4] = expected4;
            ht[key5] = expected5;

            Assert.AreEqual(expected1, ht[key1]);
            Assert.AreEqual(expected2, ht[key2]);
            Assert.AreEqual(expected3, ht[key3]);
            Assert.AreEqual(expected4, ht[key4]);
            Assert.AreEqual(expected5, ht[key5]);
        }

        [TestMethod]
        public void GetUnexistedElementTest()
        {
            MyHashTable<int, int[]> ht = new MyHashTable<int, int[]>(2);
            int key = 11;
            int[] expected = default(int[]);
            ht[1] = new int[]{ 1, 2, 3};
            ht[2] = new int[] { 4, 5, 6 };
            ht[3] = new int[] { 7, 8, 9 };

            int[] actual = ht[key];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IncludeTrueTest()
        {
            MyHashTable<string, int> ht = new MyHashTable<string, int>(5);
            string key = "ABC";
            ht[key] = 22;
            bool expected = true;

            bool actual = ht.Include(key);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IncludeFalseTest()
        {
            MyHashTable<string, int> ht = new MyHashTable<string, int>(5);
            string key = "ABC";
            ht[key] = 22;
            bool expected = false;

            bool actual = ht.Include(key + "E");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IncludeNullTest()
        {
            MyHashTable<string, int> ht = new MyHashTable<string, int>(5);
            string key = "QWERTY";
            ht[key] = 22;
            bool expected = false;

            bool actual = ht.Include(null);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveSmallTest()
        {
            MyHashTable<string, int> ht = new MyHashTable<string, int>(5);
            string key = "ABC";
            ht[key] = 22;
            bool expected = false;

            ht.Remove(key);
            bool actual = ht.Include(key);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveBigTest()
        {
            MyHashTable<string, int> ht = new MyHashTable<string, int>(2);
            bool expected = false;

            for (int i = 0; i < 20; i++)
            {
                ht[i.ToString()] = i;
            }

            ht.Remove("2");
            bool actual = ht.Include("2");

            Assert.AreEqual(expected, actual);
        }
    }
}