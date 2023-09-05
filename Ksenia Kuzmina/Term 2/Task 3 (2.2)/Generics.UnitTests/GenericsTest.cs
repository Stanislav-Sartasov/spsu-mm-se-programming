using System;
using NUnit.Framework;
using Generics;

namespace Generics.UnitTests
{
	public class Tests
	{
		[Test]
		public void AddFirstTest()
		{
			DoublyList<int> list = new DoublyList<int>();

			for (int i = 1; i <= 10; i++)
			{
				list.AddFirst(i);
			}
			Assert.AreEqual(list.Head.Data, 10);
			Assert.AreEqual(list.Tail.Data, 1);

			Assert.Pass();
		}

		[Test]
		public void AddLastTest()
		{
			DoublyList<int> list = new DoublyList<int>();

			for (int i = 1; i <= 10; i++)
			{
				list.AddLast(i);
			}
			Assert.AreEqual(list.Head.Data, 1);
			Assert.AreEqual(list.Tail.Data, 10);
		}

		[Test]
		public void RemoveFirstTest()
		{
			DoublyList<int> list = new DoublyList<int>();
			list.RemoveFirst();

			for (int i = 1; i <= 5; i++)
			{
				list.AddLast(i);
			}

			list.RemoveFirst();
			Assert.AreEqual(list.Head.Data, 2);
			Assert.Pass();
		}

		[Test]
		public void RemoveLastTest()
		{
			DoublyList<int> list = new DoublyList<int>();
			list.RemoveLast();

			for (int i = 1; i <= 5; i++)
			{
				list.AddLast(i);
			}

			list.RemoveLast();
			Assert.AreEqual(list.Tail.Data, 4);
			Assert.Pass();
		}

		[Test]
		public void RemoveTest()
		{
			DoublyList<int> list = new DoublyList<int>();
			list.Remove(5);
			for (int i = 1; i <= 5; i++)
			{
				list.AddLast(i);
			}
			list.Remove(3);
			list.Remove(6);
			Assert.AreEqual(list.Head.Next.Next.Data, 4);

			Assert.Pass();
		}

		[Test]
		public void ClearTest()
		{
			DoublyList<int> list = new DoublyList<int>();
			for (int i = 1; i <= 5; i++) ;
			list.Clear();
		}

		[Test]
		public void FindTest()
		{
			DoublyList<int> list = new DoublyList<int>();
			for (int i = 1; i <= 5; i++)
			{
				list.AddLast(i);
			}
			Assert.AreEqual(list.Find(5), 4);
			Assert.AreEqual(list.Find(6), -1);
			Assert.Pass();
		}

		[Test]
		public void FindLastTest()
		{
			DoublyList<int> list = new DoublyList<int>();
			for (int i = 1; i <= 5; i++)
			{
				list.AddLast(i);
			}
			Assert.AreEqual(list.FindLast(5), 0);
			Assert.AreEqual(list.FindLast(6), -1);
			Assert.Pass();
		}

		[Test]
		public void GetDataTest()
		{
			DoublyList<int> list = new DoublyList<int>();
			for (int i = 1; i <= 5; i++)
			{
				list.AddLast(i);
			}
			Assert.AreEqual(list.GetData(4), 5);
			Assert.Catch<IndexOutOfRangeException>(() => list.GetData(10));
			Assert.Pass();
		}

		[Test]
		public void GetDataLastTest()
		{
			DoublyList<int> list = new DoublyList<int>();
			for (int i = 1; i <= 5; i++)
			{
				list.AddLast(i);
			}
			Assert.AreEqual(list.GetDataLast(4), 1);
			Assert.Catch<IndexOutOfRangeException>(() => list.GetDataLast(10));
			Assert.Pass();
		}
	}
}