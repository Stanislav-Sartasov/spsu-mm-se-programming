using System;
using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class CopyToTests
	{
		private DynamicArray<string> _array;

		[SetUp]
		public void SetUp()
		{
			_array = new DynamicArray<string>();
		}

		[Test]
		public void CopyToTest()
		{
			_array.Add("string 1");
			_array.Add("string 2");
			_array.Add("string 3");

			var usualArray = new string[100];

			_array.CopyTo(usualArray, 30);

			Assert.AreEqual(usualArray[30], "string 1");
			Assert.AreEqual(usualArray[31], "string 2");
			Assert.AreEqual(usualArray[32], "string 3");

			Assert.Pass();
		}

		[Test]
		public void CopyToOutOfBoundsTest()
		{
			_array.Add("string 1");
			_array.Add("string 2");
			_array.Add("string 3");

			var usualArray = new string[20];

			try
			{
				_array.CopyTo(usualArray, 30);
			}
			catch (ArgumentException)
			{
				Assert.Pass();
			}

			Assert.Fail();
		}
	}
}