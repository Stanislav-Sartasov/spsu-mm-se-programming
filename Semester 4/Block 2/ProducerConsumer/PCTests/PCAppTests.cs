using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCApp;

namespace PCTests
{
	[TestClass]
	public class PCAppTests
	{
		[TestMethod]
		public void ArgumentParserTest()
		{
			var natural = "71";
			Assert.AreEqual(71, ArgumentsParser.Parse(natural));
		}

		[TestMethod]
		public void ArgumentParserExceptionTest()
		{
			var nul = "0";
			Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(nul));

			var negative = "-3";
			Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(negative));

			var fl = "36.6";
			Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(fl));

			var text = "lorem ipsum";
			Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(text));
		}
	}
}
