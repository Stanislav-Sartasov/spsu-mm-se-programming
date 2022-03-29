using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class RemoveTests
	{
		private DynamicArray<string> _array;

		[SetUp]
		public void SetUp()
		{
			_array = new DynamicArray<string>();
			_array.Add("string 1");
			_array.Add("string 2");
			_array.Add("string 3");
		}

		[Test]
		public void PossibleRemoveTest()
		{
			Assert.AreEqual(_array.Remove("string 2"), true);

			Assert.AreEqual(_array.Count, 2);
			Assert.AreEqual(_array[0], "string 1");
			Assert.AreEqual(_array[1], "string 3");

			Assert.Pass();
		}

		[Test]
		public void ImossibleRemoveTest()
		{
			Assert.AreEqual(_array.Remove("string 5"), false);
			Assert.AreEqual(_array.Count, 3);

			Assert.Pass();
		}
	}
}