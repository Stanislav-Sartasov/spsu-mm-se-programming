using System;
using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class AddTest
	{
		private DynamicArray<string> _array;

		[SetUp]
		public void SetUp()
		{
			_array = new DynamicArray<string>();
		}

		[Test]
		public void StringAdditionTest()
		{
			_array.Add("string 1");
			_array.Add("string 2");
			_array.Add("string 3");

			Assert.AreEqual(_array[0], "string 1");
			Assert.AreEqual(_array[1], "string 2");
			Assert.AreEqual(_array[2], "string 3");

			Assert.Pass();
		}

		[Test]
		public void HundredStringsAdditionTest()
		{
			for (var i = 0; i < 100; i++)
				_array.Add("string " + Convert.ToString(i + 1));

			for (var i = 0; i < 100; i++)
				Assert.AreEqual(_array[i], "string " + Convert.ToString(i + 1));

			Assert.Pass();
		}
	}
}