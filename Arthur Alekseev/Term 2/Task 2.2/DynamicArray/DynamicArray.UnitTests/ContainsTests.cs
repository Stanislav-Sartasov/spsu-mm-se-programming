using System;
using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class ContainsTests
	{
		private DynamicArray<string> _array;

		[SetUp]
		public void SetUp()
		{
			_array = new DynamicArray<string>();

			Assert.Pass();
		}

		[Test]
		public void EmptyContainsTest()
		{
			Assert.AreEqual(_array.Contains("string 19"), false);
		}

		[Test]
		public void ContainsTest()
		{
			for (var i = 0; i < 100; i++)
				_array.Add("string " + Convert.ToString(i + 1));

			Assert.AreEqual(_array.Contains("string 19"), true);
			Assert.AreEqual(_array.Contains("string 1221"), false);

			Assert.Pass();
		}
	}
}