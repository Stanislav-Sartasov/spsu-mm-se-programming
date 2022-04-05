using NUnit.Framework;
using System;

namespace DoublyLinkedList.UnitTests
{
	public class DoublyLinkedListTests
	{
		private DoublyLinkedList<int> List;

		[Test]
		public void AddTest()
		{
			List = new DoublyLinkedList<int>();

			for(int counter = 0; counter < 10; counter++)
			{
				List.Add(counter);
			}
			for (int counter = 100; counter < 120; counter++)
			{
				List.Add(counter);
			}

			for (int counter = 0; counter < 10; counter++)
			{
				Assert.AreEqual(List.FindData(counter + 1), counter);
			}
			for (int counter = 10; counter < 30; counter++)
			{
				Assert.AreEqual(List.FindData(counter + 1), 90 + counter);
			}
			Assert.Pass();
		}

		[Test]
		public void FindDataTest()
		{
			Assert.AreEqual(List.FindData(1), 0);
			Assert.AreEqual(List.FindData(10), 9);
			Assert.AreEqual(List.FindData(5), 4);
			Assert.AreEqual(List.FindData(24), 113);
			Assert.AreEqual(List.FindData(30), 119);
			Assert.Pass();
		}

		[Test]
		public void FindIndexTest()
		{
			Assert.AreEqual(List.FindIndex(0), 1);
			Assert.AreEqual(List.FindIndex(3), 4);
			Assert.AreEqual(List.FindIndex(100), 11);
			Assert.AreEqual(List.FindIndex(116), 27);
			Assert.AreEqual(List.FindIndex(119), 30);
			Assert.Pass();
		}

		[Test]
		public void RemoveTest()
		{
			List.Remove(1);
			Assert.AreEqual(List.FindData(1), 1);

			List.Remove(10);
			Assert.AreEqual(List.FindData(10), 101);

			List.Remove(28);
			Assert.AreEqual(List.FindData(27), 118);

			List.Remove(1);
			Assert.AreEqual(List.FindData(1), 2);
			List.Remove(1);
			Assert.AreEqual(List.FindData(1), 3);
			List.Remove(1);
			Assert.AreEqual(List.FindData(1), 4);
			List.Remove(1);
			Assert.AreEqual(List.FindData(1), 5);

			Assert.Pass();
		}
	}
}