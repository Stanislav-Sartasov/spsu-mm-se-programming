using System;
using NUnit.Framework;

namespace DoublyLinkedList.UnitTests
{
	public class SetTests
	{
		private DoublyLinkedList<int> _list;

		[SetUp]
		public void SetUp()
		{
			_list = new DoublyLinkedList<int>();
		}
		
		[Test]
		public void SetEmptyTest()
		{
			Assert.Throws<IndexOutOfRangeException>(() => _list[0] = 1);
		}
		
		[Test]
		public void SetOutOfRangeTest()
		{
			_list.Add(1);
			
			Assert.Throws<IndexOutOfRangeException>(() => _list[99] = 100);
		}
		
		[Test]
		public void SetTest()
		{
			DoublyLinkedList<int> list = new DoublyLinkedList<int>();
			
			for (int i = 0; i < 3; i++)
				list.Add(i);

			list[2] = 10;
			
			Assert.AreEqual(list[2], 10);
		}
	}
}

