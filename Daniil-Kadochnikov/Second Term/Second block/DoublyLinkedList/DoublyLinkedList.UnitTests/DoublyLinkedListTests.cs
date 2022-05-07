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
				Assert.AreEqual(list.FindData(counter), counter);
			}
			for (int counter = 10; counter < 30; counter++)
			{
				Assert.AreEqual(list.FindData(counter), 90 + counter);
			}
		}

		[Test]
		public void FindDataTest()
		{
			Assert.AreEqual(list.FindData(0), 0);
			Assert.AreEqual(list.FindData(10), 100);
			Assert.AreEqual(list.FindData(5), 5);
			Assert.AreEqual(list.FindData(23), 113);
			Assert.AreEqual(list.FindData(29), 119);
		}

		[Test]
		public void FindIndexTest()
		{
			Assert.AreEqual(list.FindIndex(0), 0);
			Assert.AreEqual(list.FindIndex(4), 4);
			Assert.AreEqual(list.FindIndex(101), 11);
			Assert.AreEqual(list.FindIndex(116), 26);
			Assert.AreEqual(list.FindIndex(119), 29);
		}

		[Test]
		public void RemoveTest()
		{
			list.Remove(0);
			Assert.AreEqual(list.FindData(0), 1);
			list.Remove(0);
			Assert.AreEqual(list.FindData(0), 2);
			list.Remove(0);
			Assert.AreEqual(list.FindData(0), 3);
			list.Remove(0);
			Assert.AreEqual(list.FindData(0), 4);
			list.Remove(0);
			Assert.AreEqual(list.FindData(0), 5);

			Assert.AreEqual(list.FindData(9), 104);
			list.Remove(9);
			Assert.AreEqual(list.FindData(9), 105);

			Assert.AreEqual(list.FindData(22), 118);
			list.Remove(22);
			Assert.AreEqual(list.FindData(22), 119);

			Assert.AreEqual(list.FindData(13), 109);
			list.Remove(13);
			Assert.AreEqual(list.FindData(13), 110);
		}
	}
}