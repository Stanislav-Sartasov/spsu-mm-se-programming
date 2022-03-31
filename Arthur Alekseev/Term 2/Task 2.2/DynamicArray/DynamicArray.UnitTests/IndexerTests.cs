using System;
using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class IndexerTests
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
		public void SetTest()
		{
			_array[2] = "new string 3";
			Assert.AreEqual(_array[2], "new string 3");

			Assert.Pass();
		}

		[Test]
		public void SetErrorTest()
		{
			try
			{
				_array[3] = "new string 3";
			}
			catch (ArgumentOutOfRangeException)
			{
				Assert.Pass();
			}

			Assert.Fail();
		}

		[Test]
		public void GetTest()
		{
			Assert.AreEqual(_array[2], "string 3");

			Assert.Pass();
		}

		[Test]
		public void GetErrorTest()
		{
			try
			{
				var result = _array[3];
			}
			catch (ArgumentOutOfRangeException)
			{
				Assert.Pass();
			}

			Assert.Fail();
		}
	}
}