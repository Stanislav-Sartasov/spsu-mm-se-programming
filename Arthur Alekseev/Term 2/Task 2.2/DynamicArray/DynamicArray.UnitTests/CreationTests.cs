using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class CreationTests
	{
		[Test]
		public void StringArrayCreate()
		{
			var array = new DynamicArray<string>();

			Assert.IsNotNull(array);
			Assert.AreEqual(array.Count, 0);

			Assert.Pass();
		}

		[Test]
		public void IntArrayCreate()
		{
			var array = new DynamicArray<int>();

			Assert.IsNotNull(array);
			Assert.AreEqual(array.Count, 0);

			Assert.Pass();
		}
	}
}