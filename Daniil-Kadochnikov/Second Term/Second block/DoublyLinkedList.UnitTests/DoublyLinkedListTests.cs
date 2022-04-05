using NUnit.Framework;
using System;

namespace DoublyLinkedList.UnitTests
{
	public class DoublyLinkedListTests
	{
		private DoublyLinkedList<int> list;

		[Test]
		public void AddTest()
		{
			list = new DoublyLinkedList<int>();

			for(int counter = 0; counter < 10; counter++)
			{
				list.Add(counter);
			}
			for (int counter = 100; counter < 120; counter++)
			{
				list.Add(counter);
			}

			for (int counter = 0; counter < 10; counter++)
			{
				Assert.AreEqual(list.FindData(counter + 1), counter);
			}
			for (int counter = 10; counter < 30; counter++)
			{
				Assert.AreEqual(list.FindData(counter + 1), 90 + counter);
			}
			Assert.Pass();
		}

		[Test]
		public void FindDataTest()
		{
			Assert.AreEqual(list.FindData(1), 0);
			Assert.AreEqual(list.FindData(10), 9);
			Assert.AreEqual(list.FindData(5), 4);
			Assert.AreEqual(list.FindData(24), 113);
			Assert.AreEqual(list.FindData(30), 119);
			Assert.Pass();
		}

		[Test]
		public void FindIndexTest()
		{
			Assert.AreEqual(list.FindIndex(0), 1);
			Assert.AreEqual(list.FindIndex(3), 4);
			Assert.AreEqual(list.FindIndex(100), 11);
			Assert.AreEqual(list.FindIndex(116), 27);
			Assert.AreEqual(list.FindIndex(119), 30);
			Assert.Pass();
		}

		[Test]
		public void RemoveTest()
		{
			list.Remove(1);
			Assert.AreEqual(list.FindData(1), 1);

			list.Remove(10);
			Assert.AreEqual(list.FindData(10), 101);

			list.Remove(28);
			Assert.AreEqual(list.FindData(27), 118);

			list.Remove(1);
			Assert.AreEqual(list.FindData(1), 2);
			list.Remove(1);
			Assert.AreEqual(list.FindData(1), 3);
			list.Remove(1);
			Assert.AreEqual(list.FindData(1), 4);
			list.Remove(1);
			Assert.AreEqual(list.FindData(1), 5);

			Assert.Pass();
		}
	}
}