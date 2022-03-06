using NUnit.Framework;
using DoubleLinkedList;

namespace DoubleLinkedListTest
{
    public class Tests
    {
        private DoubleLinkedList<int> currentList;

        [SetUp]
        public void Setup()
        {
            currentList = new(0);
        }

        [Test]
        public void TestListFind()
        {
            Assert.IsTrue(currentList.Find(0) == true);
            Assert.IsTrue(currentList.Find(10) == false);
        }

        [Test]
        public void TestListAdd()
        {
            Assert.IsTrue(currentList.Find(10) == false);
            currentList.Add(10);
            Assert.IsTrue(currentList.Find(10) == true);
        }

        [Test]
        public void TestListRemove()
        {
            currentList.Add(10);
            Assert.IsTrue(currentList.Find(10) == true);
            currentList.Remove(10);
            Assert.IsTrue(currentList.Find(10) == false);
        }

        [Test]
        public void TestIfRemoveReturningNull()
        {
            Assert.IsTrue(currentList.Remove(0) is null);
            currentList.Add(10);
            Assert.IsFalse(currentList.Remove(10) is null);
        }

        [Test]
        public void TestToString()
        {
            Assert.IsTrue(currentList.ToString().Equals("0 "));
            currentList.Add(10);
            Assert.IsTrue(currentList.ToString().Equals("0 10 "));
        }
    }
}