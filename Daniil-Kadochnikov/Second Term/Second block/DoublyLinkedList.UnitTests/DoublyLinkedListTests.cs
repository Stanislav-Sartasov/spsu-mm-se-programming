using NUnit.Framework;
using System;

namespace DoublyLinkedList.UnitTests
{
	public class DoublyLinkedListTests
	{
		public DoublyLinkedList<int> List;

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

			DoublyLinkedNode<int> node = List.First;
			for (int counter = 0; counter < 10; counter++)
			{
				Assert.AreEqual(node.Data, counter);
				node = node.Next;
			}
			for (int counter = 100; counter < 120; counter++)
			{
				Assert.AreEqual(node.Data, counter);
				node = node.Next;
			}

			Assert.AreEqual(List.First.Data, 0);
			Assert.AreEqual(List.Last.Data, 119);
			Assert.Pass();
		}

		[Test]
		public void FindTest()
		{
			DoublyLinkedNode<int> node = List.Find(0);
			Assert.AreEqual(node.Data, 0);
			Assert.AreEqual(node.Data, List.First.Data);

			node = List.Find(107);
			Assert.AreEqual(node.Data, 107);

			node = List.Find(10);
			Assert.AreEqual(node, null);

			node = List.Find(120);
			Assert.AreEqual(node, null);

			node = List.Find(100);
			Assert.AreEqual(node.Data, 100);
			Assert.Pass();
		}

		[Test]
		public void RemoveTest()
		{
			List.Remove(0);
			Assert.AreEqual(List.First.Data, 1);

			List.Remove(100);
			Assert.AreEqual(List.Find(100), null);

			List.Remove(119);
			Assert.AreEqual(List.Last.Data, 118);

			Assert.AreEqual(List.Remove(10), null);
			Assert.Pass();
		}
	}
}