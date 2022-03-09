using NUnit.Framework;
using System;

namespace Utils.Parser.UnitTests
{
	public class ParserTests
	{
		private Parser? parser;
		private readonly byte[] bytes = { 228, 133, 148, 10, 30, 56 };

		[Test]
		public void ConstructorTest()
		{
			try
			{
				parser = new Parser(bytes);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}

			Assert.Pass();
		}

		[Test]
		public void ParseIntTest()
		{
			int expectedValue = bytes[0] + bytes[1] * (1 << 8) + bytes[2] * (1 << 16) + bytes[3] * (1 << 24);
			int? actualValue = parser?.ParseInt(0);

			Assert.AreEqual(expectedValue, actualValue);

			Assert.Pass();
		}

		[Test]
		public void ParseShortTest()
		{
			short expectedValue = (short)(bytes[3] + bytes[4] * (1 << 8));
			short? actualValue = parser?.ParseShort(3);

			Assert.AreEqual(expectedValue, actualValue);

			Assert.Pass();
		}
	}
}