using System;
using NUnit.Framework;
using Task_3;

namespace Task_3.UnitTests
{
	public class Tests
	{
		private DynamicArray<int> currentArray;

		[SetUp]
		public void Setup()
		{
			currentArray = new DynamicArray<int>();
		}

		[Test]
		public void AddTest()
		{
			for (int i = 2; i <= 16; i *= 2)
				currentArray.Add(i);

			Assert.AreEqual(currentArray[0], 2);
			Assert.AreEqual(currentArray[1], 4);
			Assert.AreEqual(currentArray[2], 8);
			Assert.AreEqual(currentArray[3], 16);

			Assert.Pass();
		}

		[Test]
		public void RemoveAtTest()
		{
			for (int i = 2; i <= 16; i *= 2)
				currentArray.Add(i);

			currentArray.RemoveAt(3);
			Assert.AreEqual(currentArray.Count, 3);
			currentArray.RemoveAt(0);
			Assert.AreEqual(currentArray.Count, 2);
			currentArray.RemoveAt(1);
			Assert.AreEqual(currentArray.Count, 1);
			Assert.Throws<IndexOutOfRangeException>(() => currentArray.RemoveAt(10));

			Assert.Pass();
		}

		[Test]
		public void RemoveTest()
		{
			for (int i = 2; i <= 32; i *= 2)
				currentArray.Add(i);

			Assert.AreEqual(currentArray.Remove(24), false);
			Assert.AreEqual(currentArray.Remove(32), true);
			Assert.AreEqual(currentArray.Contains(32), false);

			Assert.Pass();
		}

		[Test]
		public void ClearTest()
		{
			for (int i = 2; i <= 32; i *= 2)
				currentArray.Add(i);

			Assert.AreEqual(currentArray.Count, 5);
			currentArray.Clear();
			Assert.AreEqual(currentArray.Count, 0);
			currentArray.Clear();
			Assert.AreEqual(currentArray.Count, 0);

			Assert.Pass();
		}

		[Test]
		public void ContainsTest()
		{
			for (int i = 2; i <= 16; i *= 2)
				currentArray.Add(i);

			Assert.AreEqual(currentArray.Contains(2), true);
			Assert.AreEqual(currentArray.Contains(3), false);

			Assert.Pass();
		}

		[Test]
		public void IndexOfTest()
		{
			for (int i = 2; i <= 32; i *= 2)
				currentArray.Add(i);

			Assert.AreEqual(currentArray.IndexOf(24), -1);
			Assert.AreEqual(currentArray.IndexOf(32), 4);

			Assert.Pass();
		}

		[Test]
		public void GetSetIndexerTest()
		{
			for (int i = 2; i <= 32; i *= 2)
				currentArray.Add(i);

			Assert.Throws<IndexOutOfRangeException>(() => { var tmp = currentArray[5]; });
			Assert.Throws<IndexOutOfRangeException>(() => { currentArray[5] = 64; });
			currentArray[4] = 64;
			Assert.AreEqual(currentArray[4], 64);

			Assert.Pass();
		}
	}
}