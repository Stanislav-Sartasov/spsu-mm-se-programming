using NUnit.Framework;

namespace Task3.UnitTests
{
    public class DoublyLinkedListTests
    {
        [Test]
        public void AddTest()
        {
            var list = new DoublyLinkedList<int>();
            list.Add(1);
            Assert.AreEqual(list.Length, 1);
            list.Clear();
        }

        [Test]
        public void ClearTest()
        {
            var list = new DoublyLinkedList<int>();
            for (int i = 0; i < 1000 - 7; i++)
            {
                list.Add(i);
            }
            list.Clear();
            Assert.AreEqual(list.Length, 0);
        }

        [Test]
        public void FindTest()
        {
            var list = new DoublyLinkedList<int>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(i);
            }
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(list.Find(i), i);
            }
            list.Clear();
        }

        [Test]
        public void RemoveWithTest()
        {
            var list = new DoublyLinkedList<int>();
            list.Add(1);
            Assert.IsTrue(list.RemoveWith(1));
            Assert.AreEqual(list.Length, 0);
            list.Clear();
        }

        [Test]
        public void RemoveAtTest()
        {
            var list = new DoublyLinkedList<int>();
            list.Add(10);
            list.Add(20);
            list.Add(30);
            list.RemoveAt(1);
            Assert.AreEqual(list.Find(1), 30);
            Assert.AreEqual(list.Length, 2);
            list.RemoveAt(1);
            Assert.AreEqual(list.Length, 1);
            list.RemoveAt(0);
            Assert.AreEqual(list.Length, 0);
            list.Clear();
        }

        [Test]
        public void RemoveAllTest()
        {
            var list = new DoublyLinkedList<int>();
            for (int i = 0; i < 1000 - 7; i++)
            {
                list.Add(0);
            }
            Assert.AreEqual(list.Length, 1000 - 7);
            Assert.AreEqual(list.RemoveAll(0), 1000 - 7);
            Assert.AreEqual(list.Length, 0);
            list.Clear();
        }

        [Test]
        public void FindIndexTest()
        {
            var list = new DoublyLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
            Assert.AreEqual(list.FindIndex(5), 5);
            list.Clear();
        }

        [Test]
        public void AddAtIndexTest()
        {
            var list = new DoublyLinkedList<int>();
            for (int i = 1; i < 5; i++)
            {
                list.Add(i - 1, i);
            }
            for (int i = 1; i < 5; i++)
            {
                Assert.AreEqual(list.Find(i - 1), i);
            }
            list.Add(0, 0);
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(list.Find(i), i);
            }
            list.Clear();
        }

        [Test]
        public void AddExeptionTest()
        {
            var list = new DoublyLinkedList<int>();
            Assert.Catch<System.ArgumentOutOfRangeException>(() => list.Add(-1, 0));
        }

        [Test]
        public void FindExeptionTest()
        {
            var list = new DoublyLinkedList<int>();
            Assert.Catch<System.ArgumentOutOfRangeException>(() => list.Find(-1));
        }
    }
}