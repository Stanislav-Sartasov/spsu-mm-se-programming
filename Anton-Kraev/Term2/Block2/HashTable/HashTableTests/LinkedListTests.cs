using NUnit.Framework;
using HashTable;

namespace HashTableTests
{
    public class LinkedListTests
    {
        LinkedList<int> linkedList;

        [SetUp]
        public void Setup()
        {
            linkedList = new LinkedList<int>();
        }

        [Test]
        public void AddFirstElementTest()
        {
            linkedList.Add(5);
            Assert.AreEqual(5, linkedList.Head.Data);
            Assert.AreEqual(1, linkedList.Count);
        }

        [Test]
        public void AddSomeElementsTest()
        {
            for (int i = 0; i < 20; i++)
            {
                linkedList.Add(i);
            }

            var current = linkedList.Head;
            for (int i = 0; i < 20; i++)
            {
                Assert.AreEqual(current.Data, i);
                current = current.Next;
            }
            Assert.AreEqual(linkedList.Count, 20);
        }

        [Test]
        public void AddEqualElementsTest()
        {
            for (int i = 0; i < 10; i++)
            {
                linkedList.Add(1);
            }

            Assert.AreEqual(linkedList.Head.Data, 1);
            Assert.AreEqual(linkedList.Head.Next, null);
            Assert.AreEqual(linkedList.Count, 1);
        }

        [Test]
        public void DeleteFirstElementTest()
        {
            linkedList.Add(1);
            linkedList.Add(2);
            linkedList.Delete(1);

            Assert.AreEqual(linkedList.Head.Data, 2);
            Assert.AreEqual(linkedList.Head.Next, null);
            Assert.AreEqual(linkedList.Count, 1);
        }

        [Test]
        public void DeleteLastElementTest()
        {
            linkedList.Add(1);
            linkedList.Add(2);
            linkedList.Add(3);
            linkedList.Delete(3);

            Assert.AreEqual(linkedList.Head.Data, 1);
            Assert.AreEqual(linkedList.Head.Next.Data, 2);
            Assert.AreEqual(linkedList.Head.Next.Next, null);
            Assert.AreEqual(linkedList.Count, 2);
        }

        [Test]
        public void SearchTest()
        {
            for (int i = 0; i < 10; i++)
            {
                linkedList.Add(i * 2);
            }

            for (int i = 0; i < 10; i += 2)
            {
                Assert.IsTrue(linkedList.Search(i));
                linkedList.Delete(i);
                Assert.IsFalse(linkedList.Search(i));
            }
            Assert.IsTrue(linkedList.Search(18));
            Assert.IsFalse(linkedList.Search(17));
        }

        [Test]
        public void CloneAccuracyTest()
        {
            var x = new LinkedList<string>();
            x.Add("abc");
            x.Add("def");
            var y = (LinkedList<string>) x.Clone();

            Assert.AreEqual(x.Head.Data, y.Head.Data);
            Assert.AreEqual(x.Head.Next.Data, y.Head.Next.Data);
            Assert.AreEqual(x.Count, y.Count);
        }

        [Test]
        public void DeepCopyTest()
        {
            var x = new LinkedList<string>();
            x.Add("abc");
            var y = (LinkedList<string>)x.Clone();
            x.Add("def");
            x.Delete("abc");

            Assert.AreEqual("def", x.Head.Data);
            Assert.AreEqual(null, x.Head.Next);
            Assert.AreEqual("abc", y.Head.Data);
            Assert.AreEqual(null, y.Head.Next);
        }

        [Test]
        public void EnumeratorTest()
        {
            for (int i = 0; i < 10; i++)
                linkedList.Add(i);
            var sum = 0;
            foreach (var number in linkedList)
            {
                sum += number;
            }

            Assert.AreEqual(45, sum);
        }
    }
}