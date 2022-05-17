using Microsoft.VisualStudio.TestTools.UnitTesting;
using RouletteLib;
using LibLoader;

namespace LibLoaderTests
{
	[TestClass]
	public class BotsLibLoaderTests
	{
		[TestMethod]
		public void WrongLoadTest()
		{
			BetEssence bet = new ColourBet(ColourBetsEnum.Red);
			BotsLibLoader loader = new BotsLibLoader();
			Assert.IsFalse(loader.Load(@"..\..\..\..\BotsLib\BotsLib222.dll", bet, 5000));
		}

		[TestMethod]
		public void CorrectLoadTest()
		{
			BetEssence bet = new ColourBet(ColourBetsEnum.Red);
			BotsLibLoader loader = new BotsLibLoader();
			Assert.IsTrue(loader.Load(@"..\..\..\..\BotsLib\BotsLib.dll", bet, 5000));
		}
	}
}
