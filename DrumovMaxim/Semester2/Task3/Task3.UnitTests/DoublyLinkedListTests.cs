using NUnit.Framework;

namespace Task3.UnitTests
{
    public class DoublyLinkedListTests
    {
        [Test]
        public void AddTest()
        {
            DoublyLinkedList<int> listTest = new DoublyLinkedList<int>();
            for (int i = 0; i < 3; i++)
            {
                listTest.Add(i);
            }
            Assert.AreEqual(listTest.Length, 3);
            listTest.Clear();
        }

        [Test]
        public void FindIndexTest()
        {
            DoublyLinkedList<int> listTest = new DoublyLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                listTest.Add(i);
            }
            for (int i = 30; i < 40; i++)
            {
                listTest.Add(i);
            }
            Assert.AreEqual(listTest.FindIndex(3), 3);
            Assert.AreEqual(listTest.FindIndex(5), 5);
            Assert.AreEqual(listTest.FindIndex(7), 7);
            Assert.AreEqual(listTest.FindIndex(31), 11);
            Assert.AreEqual(listTest.FindIndex(33), 13);
            Assert.AreEqual(listTest.FindIndex(37), 17);
            Assert.AreEqual(listTest.FindIndex(39), 19);
            listTest.Clear();
        }

        [Test]
        public void FindAtIndexTest()
        {
            DoublyLinkedList<int> listTest = new DoublyLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                listTest.Add(i);
            }
            for (int i = 30; i < 40; i++)
            {
                listTest.Add(i);
            }
            Assert.AreEqual(listTest.FindAtIndex(2), 2);
            Assert.AreEqual(listTest.FindAtIndex(3), 3);
            Assert.AreEqual(listTest.FindAtIndex(5), 5);
            Assert.AreEqual(listTest.FindAtIndex(7), 7);
            Assert.AreEqual(listTest.FindAtIndex(11), 31);
            Assert.AreEqual(listTest.FindAtIndex(13), 33);
            Assert.AreEqual(listTest.FindAtIndex(17), 37);
            listTest.Clear();
        }
        [Test]
        public void RemoveTest()
        {
            DoublyLinkedList<int> listTest = new DoublyLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                listTest.Add(i);
            }

            listTest.Remove(0);
            Assert.AreEqual(listTest.FindAtIndex(0), 1);
            listTest.Remove(1);
            Assert.AreEqual(listTest.FindAtIndex(1), 3);
            Assert.AreEqual(listTest.Length, 8);

            while(listTest.Length != 0)
            {
                for (int i = 0; i < listTest.Length; i++)
                {
                    listTest.Remove(i);
                }
            }

            Assert.AreEqual(listTest.Length, 0);
            listTest.Clear();
        }
        [Test]
        public void ClearTest()
        {
            DoublyLinkedList<int> listTest = new DoublyLinkedList<int>();
            for (int i = 0; i < 1000; i++)
            {
                listTest.Add(i);
            }
            Assert.AreEqual(listTest.Length, 1000);
            listTest.Clear();
            Assert.AreEqual(listTest.Length, 0);
        }
        [Test]
        public void FindAtIndexExceptionTest()
        {
            DoublyLinkedList<int> listTest = new DoublyLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                listTest.Add(i);
            }
            Assert.Catch<System.ArgumentOutOfRangeException>(() => listTest.FindAtIndex(-1));
            listTest.Clear();
        }
        [Test]
        public void RemoveExceptionTest()
        {
            DoublyLinkedList<int> listTest = new DoublyLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                listTest.Add(i);
            }
            Assert.Catch<System.ArgumentOutOfRangeException>(() => listTest.Remove(-1));
            listTest.Clear();
        }
    }
}