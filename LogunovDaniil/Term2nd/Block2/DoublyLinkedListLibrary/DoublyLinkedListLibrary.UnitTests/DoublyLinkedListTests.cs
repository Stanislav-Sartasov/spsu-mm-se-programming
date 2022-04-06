using NUnit.Framework;
using DoublyLinkedListLibrary;

namespace DoublyLinkedListLibrary.UnitTests
{
	public class DoublyLinkedListTests
	{
		[Test]
		public void AddValueTest()
		{
			DoublyLinkedList<int> list = new();
			int one = 1;
			list.Add(one);
			Assert.AreEqual(one, list.GetAt(0));
		}

		[Test]
		public void GetAtTest()
		{
			DoublyLinkedList<int> list = new();
			for (int i = 0; i < 5; i++)
			{
				list.Add(i);
			}
			Assert.AreEqual(4, list.GetAt(4));
		}

		[Test]
		public void RemoveValueTest()
		{
			DoublyLinkedList<int> list = new();
			for (int i = 0; i < 5; i++)
			{
				list.Add(i);
			}
			list.Remove(2);
			Assert.AreEqual(3, list.GetAt(2));
		}

		[Test]
		public void RemoveAtValueTest()
		{
			DoublyLinkedList<int> list = new();
			for (int i = 0; i < 5; i++)
			{
				list.Add(i);
			}
			list.RemoveAt(2);
			Assert.AreEqual(3, list.GetAt(2));
		}

		[Test]
		public void GetIndexTest()
		{
			DoublyLinkedList<int> list = new();
			for (int i = 0; i < 5; i++)
			{
				list.Add(i);
			}
			Assert.AreEqual(2, list.GetIndex(2));
		}

		[Test]
		public void GetIndexLastTest()
		{
			DoublyLinkedList<int> list = new();
			for (int i = 0; i < 5; i++)
			{
				list.Add(i);
			}
			list.Add(0);
			Assert.AreEqual(5, list.GetIndexLast(0));
		}

		[Test]
		public void RemoveLastTest()
		{
			DoublyLinkedList<int> list = new();
			for (int i = 0; i < 5; i++)
			{
				list.Add(i);
			}
			list.Add(0);
			list.RemoveLast(0);
			Assert.AreEqual(0, list.GetIndexLast(0));
		}

		[Test]
		public void RemoveAllElementsTest()
		{
			DoublyLinkedList<int> list = new();
			for (int i = 0; i < 5; i++)
			{
				list.Add(i);
			}
			for (int i = 0; i < 5; i++)
			{
				list.Remove(i);
			}
			for (int i = 0; i < 5; i++)
				Assert.AreEqual(-1, list.GetIndexLast(i));
		}

		[Test]
		public void RemoveNonExistingElementTest()
		{
			DoublyLinkedList<int> list = new();
			for (int i = 0; i < 5; i++)
			{
				list.Add(i);
			}
			list.Remove(5);
			for (int i = 0; i < 5; i++)
				Assert.AreEqual(i, list.GetIndex(i));
			Assert.AreEqual(5, list.Count);
		}
	}
}
