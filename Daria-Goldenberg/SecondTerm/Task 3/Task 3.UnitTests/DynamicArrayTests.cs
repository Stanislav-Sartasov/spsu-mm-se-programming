using System;
using NUnit.Framework;
using Task_3;

namespace Task_3.UnitTests
{
	public class Tests
	{
		private DynamicArray<int> example;

		[SetUp]
		public void Setup()
		{
			example = new DynamicArray<int>();
		}

		[Test]
		public void AddTest()
		{
			for (int i = 2; i <= 16; i *= 2)
				example.Add(i);

			Assert.AreEqual(example[0], 2);
			Assert.AreEqual(example[1], 4);
			Assert.AreEqual(example[2], 8);
			Assert.AreEqual(example[3], 16);

			Assert.Pass();
		}

		[Test]
		public void RemoveAtTest()
		{
			for (int i = 2; i <= 16; i *= 2)
				example.Add(i);

			example.RemoveAt(3);
			Assert.AreEqual(example.Count, 3);
			example.RemoveAt(0);
			Assert.AreEqual(example.Count, 2);
			example.RemoveAt(1);
			Assert.AreEqual(example.Count, 1);
			Assert.Throws<ArgumentOutOfRangeException>(() => example.RemoveAt(10));

			Assert.Pass();
		}

		[Test]
		public void RemoveTest()
		{
			for (int i = 2; i <= 32; i *= 2)
				example.Add(i);

			Assert.AreEqual(example.Remove(24), false);
			Assert.AreEqual(example.Remove(32), true);
			Assert.AreEqual(example.Contains(32), false);

			Assert.Pass();
		}

		[Test]
		public void ClearTest()
		{
			for (int i = 2; i <= 32; i *= 2)
				example.Add(i);

			Assert.AreEqual(example.Count, 5);
			example.Clear();
			Assert.AreEqual(example.Count, 0);
			example.Clear();
			Assert.AreEqual(example.Count, 0);

			Assert.Pass();
		}

		[Test]
		public void ContainsTest()
		{
			for (int i = 2; i <= 16; i *= 2)
				example.Add(i);

			Assert.AreEqual(example.Contains(2), true);
			Assert.AreEqual(example.Contains(3), false);

			Assert.Pass();
		}

		[Test]
		public void IndexOfTest()
		{
			for (int i = 2; i <= 32; i *= 2)
				example.Add(i);

			Assert.AreEqual(example.IndexOf(24), -1);
			Assert.AreEqual(example.IndexOf(32), 4);

			Assert.Pass();
		}

		[Test]
		public void GetSetIndexerTest()
		{
			for (int i = 2; i <= 32; i *= 2)
				example.Add(i);

			Assert.Throws<ArgumentOutOfRangeException>(() => { var tmp = example[5]; });
			Assert.Throws<ArgumentOutOfRangeException>(() => { example[5] = 64; });
			example[4] = 64;
			Assert.AreEqual(example[4], 64);

			Assert.Pass();
		}
	}
}