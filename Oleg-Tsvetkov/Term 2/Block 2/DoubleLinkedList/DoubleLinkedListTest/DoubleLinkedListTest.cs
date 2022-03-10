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
            Assert.IsTrue(currentList.Find(0));
            Assert.IsFalse(currentList.Find(10));
        }

        [Test]
        public void TestListAdd()
        {
            Assert.IsFalse(currentList.Find(10));
            currentList.Add(10);
            Assert.IsTrue(currentList.Find(10));
        }

        [Test]
        public void TestListRemove()
        {
            Assert.IsFalse(currentList.Find(10));
            currentList.Add(10);
            Assert.IsTrue(currentList.Find(10));
            currentList.Remove(10);
            Assert.IsFalse(currentList.Find(10));
        }

        [Test]
        public void TestIfRemoveReturningNull()
        {
            Assert.IsNull(currentList.Remove(0));
            currentList.Add(10);
            Assert.IsNotNull(currentList.Remove(10));
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