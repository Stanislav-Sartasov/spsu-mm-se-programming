using NUnit.Framework;

namespace DoublyLinkedList.UnitTests
{
	public class RemoveTests
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
		public void RemoveOutOfRangeTest()
		{
			_list.Remove(99);
			
			for (int i = 0; i < 10; i++)
				Assert.AreEqual(_list[i], i);
		}

		[Test]
		public void RemoveTest()
		{
			_list.Remove(5);
			
			for (int i = 0; i < 5; i++)
				Assert.AreEqual(_list[i], i);
			
			for (int i = 5; i < 9; i++)
				Assert.AreEqual(_list[i], i + 1);
		}
		
		[Test]
		public void RemoveFirst()
		{
			_list.Remove(0);
			
			for (int i = 0; i < 9; i++)
				Assert.AreEqual(_list[i], i + 1);
		}
		
		[Test]
		public void RemoveLast()
		{
			_list.Remove(9);
			
			for (int i = 0; i < 9; i++)
				Assert.AreEqual(_list[i], i);
		}
	}
}

