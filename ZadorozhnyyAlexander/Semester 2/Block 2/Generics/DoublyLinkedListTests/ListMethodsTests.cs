using NUnit.Framework;
using DoublyLinkedList;
using System;

namespace DoublyLinkedListTests
{
    public class ListMethodsTests
    {
        private DoubleLinkedListClass<int> testIntList;
        private DoubleLinkedListClass<string> testStringList;

        private void FillTheList(int[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                testIntList.Add(list[i]);
            }
        }

        private void FillTheList(string[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                testStringList.Add(list[i]);
            }
        }

        [SetUp]
        public void Setup()
        {
            testIntList = new DoubleLinkedListClass<int>();
            testStringList = new DoubleLinkedListClass<string>();
        }

        [Test]
        public void AddToIntListTest()
        {
            int[] testList = { 1, 2, 10, 4, 8, 0 };
            FillTheList(testList);
            Array.Sort(testList);
            Assert.AreEqual(testList, testIntList.GetAllElements());
        }

        [Test]
        public void AddToStringListTest()
        {
            string[] testList = { "Bill", "Dakota", "Freddy", "Meek", "Tony" };
            FillTheList(testList);
            Array.Sort(testList);
            Assert.AreEqual(testList, testStringList.GetAllElements());
        }

        [Test]
        public void RemoveFromIntListTest()
        {
            int[] result = { 0, 1, 2 };
            int[] testList = { 0, 1, 2, 4, 8, 10 };
            FillTheList(testList);
            testIntList.Remove(4);
            testIntList.Remove(8);
            testIntList.Remove(9);
            testIntList.Remove(10);
            Assert.AreEqual(result, testIntList.GetAllElements());
        }

        [Test]
        public void RemoveFromStringListTest()
        {
            string[] result = { "Bill", "Dakota", "Meek", "Tony" };
            Array.Sort(result);
            string[] testList = { "Bill", "Dakota", "Freddy", "Meek", "Tony"};
            Array.Sort(testList);
            FillTheList(testList);
            testStringList.Remove("Freddy");
            testStringList.Remove("Freddy");
            Assert.AreEqual(result, testStringList.GetAllElements());
        }

        [Test]
        public void FindElementOfIntListTest()
        {
            int[] testList = { 0, 1, 2, 4, 8, 10 };
            FillTheList(testList);
            Assert.IsTrue(testIntList.Find(2));
            Assert.IsFalse(testIntList.Find(5));
        }

        [Test]
        public void FindElementOfStringListTest()
        {
            string[] testList = { "Bill", "Dakota", "Freddy", "Meek", "Tony" };
            FillTheList(testList);
            Assert.IsTrue(testStringList.Find("Dakota"));
            Assert.IsFalse(testStringList.Find("Sam"));
        }
    }
}