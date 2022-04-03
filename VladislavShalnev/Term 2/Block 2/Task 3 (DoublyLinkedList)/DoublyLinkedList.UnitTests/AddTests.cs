using NUnit.Framework;

namespace DoublyLinkedList.UnitTests
{
	public class AddTests
	{
		private DoublyLinkedList<int> _list;

		[SetUp]
		public void SetUp()
		{
			_list = new DoublyLinkedList<int>();
		}
		
		[Test]
		public void AddTest()
		{
			for (int i = 0; i < 10; i++)
				_list.Add(i);
			
			for (int i = 0; i < 10; i++)
				Assert.AreEqual(_list[i], i);

			Assert.AreEqual(_list.Length, 10);
		}
	}
}

