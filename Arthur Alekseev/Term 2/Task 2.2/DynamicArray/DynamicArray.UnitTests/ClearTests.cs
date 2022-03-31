using System;
using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class ClearTests
	{
		private DynamicArray<string> _array;

		[SetUp]
		public void SetUp()
		{
			_array = new DynamicArray<string>();
		}

		[Test]
		public void EmptyClearTest()
		{
			_array.Clear();
			Assert.AreEqual(_array.Count, 0);
			Assert.Pass();
		}

		[Test]
		public void ClearTest()
		{
			for (var i = 0; i < 100; i++)
				_array.Add("string " + Convert.ToString(i + 1));

			_array.Clear();
			Assert.AreEqual(_array.Count, 0);
			Assert.Pass();
		}
	}
}