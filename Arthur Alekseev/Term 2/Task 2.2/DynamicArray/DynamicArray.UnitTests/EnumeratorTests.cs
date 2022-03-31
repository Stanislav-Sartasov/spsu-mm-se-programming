using System;
using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class EnumeratorTests
	{
		private DynamicArray<string> _array = new DynamicArray<string>();

		[SetUp]
		public void SetUp()
		{
			_array = new DynamicArray<string>();
		}

		[Test]
		public void ForeachTest()
		{
			_array.Add("string 1");
			_array.Add("string 2");
			_array.Add("string 3");

			var index = 0;
			foreach (var item in _array) Assert.AreEqual(item, "string " + Convert.ToString(++index));
			Assert.Pass();
		}

		[Test]
		public void EmptyForeachTest()
		{
			foreach (var item in _array) Assert.Fail();
			Assert.Pass();
		}
	}
}