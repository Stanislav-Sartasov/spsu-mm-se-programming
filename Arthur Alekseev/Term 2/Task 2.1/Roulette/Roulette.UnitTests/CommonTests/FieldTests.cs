using NUnit.Framework;
using Roulette.Common.GamePlay;

namespace Roulette.UnitTests.CommonTests
{
	public class FieldTests
	{
		[Test]
		public void FieldCreate()
		{
			var field = new Field(0, Color.Green);
			Assert.IsNotNull(field);
			Assert.Pass();
		}

		[Test]
		public void RedOneToStringTest()
		{
			var field = new Field(1, Color.Red);
			Assert.AreEqual("1 Red", field.ToString());

			Assert.Pass();
		}

		[Test]
		public void GreenZeroToStringTest()
		{
			var field = new Field(0, Color.Green);
			Assert.AreEqual("0 Green", field.ToString());

			Assert.Pass();
		}

		[Test]
		public void BlackTwoToStringTest()
		{
			var field = new Field(2, Color.Black);
			Assert.AreEqual("2 Black", field.ToString());

			Assert.Pass();
		}
	}
}