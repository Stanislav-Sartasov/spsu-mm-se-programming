using NUnit.Framework;

namespace Task3.UnitTests
{
    public class DoublyLinkedTests
    {
        [Test]
        public void AddToEndTest()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 1; i < 30; i++)
            {
                list.AddToEnd(i);
            }

            Assert.AreEqual(29, list.Length);
            Assert.AreEqual(list.GetIndexData(29), 28);
        }

        [Test]
        public void AddToStartTest()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 1; i < 17; i++)
            {
                list.AddToStart(i);
            }

            Assert.AreEqual(16, list.Length);
            Assert.AreEqual(list.GetIndexData(16), 0);
        }

        [Test]
        public void RemoveByDataTest()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            list.AddToEnd(34);
            list.AddToEnd(48);
            list.AddToEnd(33);
            Assert.IsTrue(list.RemoveByData(48));
            Assert.AreEqual(list.Length, 2);
        }

        [Test]
        public void RemoveByIndexTest()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 1; i < 67; i++)
            {
                list.AddToEnd(i);
            }

            list.RemoveByIndex(56);
            list.RemoveByIndex(34);
            list.RemoveByIndex(0);
            Assert.AreEqual(list.Length, 63);
        }

        [Test]
        public void GetDataTest()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            list.AddToEnd(10);
            list.AddToStart(23);
            Assert.AreEqual(23, list.GetData(0));
        }


        [Test]
        public void FindIndex()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            for (int i = 1; i < 17; i++)
            {
                list.AddToEnd(i);
            }

            Assert.AreEqual(list.GetIndexData(15), 14);
            Assert.AreEqual(list.GetIndexData(23), -1);
        }
    }
}