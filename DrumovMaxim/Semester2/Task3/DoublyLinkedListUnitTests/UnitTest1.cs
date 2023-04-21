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
    }
}