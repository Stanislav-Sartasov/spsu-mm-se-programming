using NUnit.Framework;

namespace Roulette.UnitTests.CommonTests
{
	public class RouletteTests
	{
		[Test]
		public void CreateTest()
		{
			var roulette = new Common.GamePlay.Roulette();
			Assert.IsNotNull(roulette);

			Assert.Pass();
		}

		[Test]
		public void NotNullFieldTest()
		{
			var roulette = new Common.GamePlay.Roulette();
			var field = roulette.GetRandomField();
			Assert.IsNotNull(field);
		}
	}
}