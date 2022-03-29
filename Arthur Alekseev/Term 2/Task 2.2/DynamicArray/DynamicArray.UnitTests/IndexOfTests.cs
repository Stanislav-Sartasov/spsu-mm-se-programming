using System;
using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class IndexOfTests
	{
		private DynamicArray<string> _array;

		[SetUp]
		public void SetUp()
		{
			_array = new DynamicArray<string>();
		}

		[Test]
		public void FindingIndexOfTest()
		{
			for (var i = 0; i < 100; i++)
				_array.Add("string " + Convert.ToString(i + 1));

			Assert.AreEqual(_array.IndexOf("string 1"), 0);
			Assert.AreEqual(_array.IndexOf("string 99"), 98);
			Assert.AreEqual(_array.IndexOf("string 32"), 31);

			Assert.Pass();
		}

		[Test]
		public void NotFindingIndexOfTest()
		{
			Assert.AreEqual(_array.IndexOf("string 1"), -1);
			Assert.AreEqual(_array.IndexOf("string 99"), -1);
			Assert.AreEqual(_array.IndexOf("string 32"), -1);

			Assert.Pass();
		}
	}
}