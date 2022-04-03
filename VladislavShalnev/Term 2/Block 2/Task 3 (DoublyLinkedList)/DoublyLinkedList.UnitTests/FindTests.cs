using NUnit.Framework;

namespace DoublyLinkedList.UnitTests
{
	public class FindTests
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
		public void FindNotExistingTest()
		{
			Assert.AreEqual(_list.Find(99), -1);
		}
		
		[Test]
		public void FindDeletedTest()
		{
			_list.Remove(5);
			
			Assert.AreEqual(_list.Find(5), -1);
		}
		
		[Test]
		public void FindTest()
		{
			Assert.AreEqual(_list.Find(5), 5);
		}
	}
}

