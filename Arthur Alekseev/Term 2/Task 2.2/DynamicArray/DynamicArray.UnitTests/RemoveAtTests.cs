using System;
using NUnit.Framework;

namespace DynamicArray.UnitTests
{
	public class RemoveAtTests
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
		public void PossibleRemoveAtTest()
		{
			_array.RemoveAt(1);

			Assert.AreEqual(_array.Count, 2);
			Assert.AreEqual(_array[0], "string 1");
			Assert.AreEqual(_array[1], "string 3");

			Assert.Pass();
		}

		[Test]
		public void ImossibleRemoveAtTest()
		{
			try
			{
				_array.RemoveAt(5);
			}
			catch (ArgumentOutOfRangeException)
			{
				Assert.Pass();
			}

			Assert.Fail();
		}
	}
}