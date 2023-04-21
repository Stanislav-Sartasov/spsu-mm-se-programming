using Microsoft.VisualStudio.TestTools.UnitTesting;
using BotsLib;
using RouletteLib;

namespace BotsLibTests
{
	[TestClass]
	public class BotMartingaleTests
	{
		BetEssence colourBet = new ColourBet(ColourBetsEnum.Red);
		BetEssence parityBet = new ParityBet(ParityBetsEnum.Even);
		BetEssence dozenBet = new DozenBet(DozenBetsEnum.First);
		BetEssence singleBet = new SingleBet(12);

		[TestMethod]
		public void ColourPlayTest()
		{
			Assert.IsTrue(PlayTest(colourBet));
		}

		[TestMethod]
		public void ParityPlayTest()
		{
			Assert.IsTrue(PlayTest(parityBet));
		}

		[TestMethod]
		public void DozenPlayTest()
		{
			Assert.IsTrue(PlayTest(dozenBet));
		}

		[TestMethod]
		public void CorrectSingleBetTest()
		{
			Assert.IsTrue(PlayTest(singleBet));
		}

		public bool PlayTest(BetEssence bet)
		{
			int startBetAmount = 10;
			int startCash = 5000;

			Bot bot = new BotMartingale(bet, startCash);

			int firstCashAfterBet = bot.Play(1);

			bool isPlayCorrect = false;
			if ((firstCashAfterBet == startCash - startBetAmount || firstCashAfterBet == startCash + bet.Coefficient * startBetAmount))
			{
				isPlayCorrect = true;
			}

			return isPlayCorrect;
		}
	}
}
