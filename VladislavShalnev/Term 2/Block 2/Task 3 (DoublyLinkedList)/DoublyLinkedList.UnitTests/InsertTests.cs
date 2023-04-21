using System;
using NUnit.Framework;

namespace DoublyLinkedList.UnitTests
{
	public class InsertTests
	{
		private DoublyLinkedList<int> _list;

		[SetUp]
		public void SetUp()
		{
			_list = new DoublyLinkedList<int>();
			
			for (int i = 0; i < 10; i++)
				_list.Add(i);
		}
		
		[Test]
		public void InsertToEmptyTest()
		{
			DoublyLinkedList<int> list = new DoublyLinkedList<int>();
			
			list.Insert(0, 10);
			
			Assert.AreEqual(list[0], 10);
		}

		[Test]
		public void InsertOutOfRangeTest()
		{
			Assert.Throws<IndexOutOfRangeException>(() => _list.Insert(99, 1));
		}
		
		[Test]
		public void InsertFirstTest()
		{
			_list.Insert(0, 10);
			
			Assert.AreEqual(_list[0], 10);
			
			for (int i = 1; i < 11; i++)
				Assert.AreEqual(_list[i], i - 1);
		}
		
		[Test]
		public void InsertLastTest()
		{
			_list.Insert(_list.Length, 10);

			for (int i = 0; i < 11; i++)
				Assert.AreEqual(_list[i], i);
		}
		
		[Test]
		public void InsertTest()
		{
			_list.Insert(5, 10);

			for (int i = 0; i < 5; i++)
				Assert.AreEqual(_list[i], i);
			
			Assert.AreEqual(_list[5], 10);
			
			for (int i = 6; i < 11; i++)
				Assert.AreEqual(_list[i], i - 1);
		}
		
	}
}

