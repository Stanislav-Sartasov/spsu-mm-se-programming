using Microsoft.VisualStudio.TestTools.UnitTesting;
using BotsLib;
using RouletteLib;

namespace BotsLibTests
{
	[TestClass]
	public class BotLabouchererTests
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
			int startCash = 5000;

			Bot bot = new BotLaboucherer(bet, startCash);

			int cashInPlay = (int)(0.1 * startCash);
			int sequenceMember = (int)(cashInPlay / 5);

			int cashAfterBet = bot.Play(1);

			int cashAfterLoss = startCash - 2 * sequenceMember;
			int cashAfterWin = startCash + bet.Coefficient * 2 * sequenceMember;

			bool isPlayCorrect = false;
			if ((cashAfterBet == cashAfterLoss || cashAfterBet == cashAfterWin))
			{
				isPlayCorrect = true;
			}

			return isPlayCorrect;
		}
	}
}
