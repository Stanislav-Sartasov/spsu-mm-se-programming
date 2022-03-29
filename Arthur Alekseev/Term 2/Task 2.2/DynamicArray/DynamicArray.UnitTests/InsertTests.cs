using System;
using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class InsertTests
	{
		private DynamicArray<string> _array;

		[SetUp]
		public void SetUp()
		{
			_array = new DynamicArray<string>();
			_array.Add("string 1");
			_array.Add("string 3");
		}

		[Test]
		public void PossibleInsertInTwoTest()
		{
			_array.Insert(1, "string 2");

			Assert.AreEqual(_array[0], "string 1");
			Assert.AreEqual(_array[1], "string 2");
			Assert.AreEqual(_array[2], "string 3");

			Assert.Pass();
		}

		[Test]
		public void PossibleInsertInOneTest()
		{
			_array.Insert(0, "string 2");

			Assert.AreEqual(_array[0], "string 2");
			Assert.AreEqual(_array[1], "string 1");
			Assert.AreEqual(_array[2], "string 3");

			Assert.Pass();
		}

		[Test]
		public void ImpossibleInsertTest()
		{
			try
			{
				_array.Insert(5, "string 5");
			}
			catch (ArgumentOutOfRangeException)
			{
				Assert.Pass();
			}

			Assert.Fail();
		}
	}
}