using System;
using NUnit.Framework;

namespace DoublyLinkedList.UnitTests
{
	public class GetTests
	{
		private DoublyLinkedList<int> _list;

		[SetUp]
		public void SetUp()
		{
			_list = new DoublyLinkedList<int>();
		}
		
		[Test]
		public void GetEmptyTest()
		{
			Assert.Throws<IndexOutOfRangeException>(() => _list[0].ToString());
		}
		
		[Test]
		public void GetOutOfRangeTest()
		{
			_list.Add(1);
			
			Assert.Throws<IndexOutOfRangeException>(() => _list[99].ToString());
		}
		
		[Test]
		public void GetTest()
		{
			for (int i = 0; i < 3; i++)
				_list.Add(i);
			
			Assert.AreEqual(_list[1], 1);
		}
	}
}

