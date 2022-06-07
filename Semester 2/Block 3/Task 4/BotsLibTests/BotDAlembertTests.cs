using Microsoft.VisualStudio.TestTools.UnitTesting;
using BotsLib;
using RouletteLib;
using System;

namespace BotsLibTests
{
	[TestClass]
	public class BotDAlembertTests
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
		public void WrongSingleBetTest()
		{
			bool isExceptionCaught = false;
			try
			{
				BetEssence bet = new SingleBet(52);
			}
			catch(Exception)
			{
				isExceptionCaught = true;
			}

			Assert.IsTrue(isExceptionCaught);
		}

		[TestMethod]
		public void CorrectSingleBetTest()
		{
			Assert.IsTrue(PlayTest(singleBet));
		}

		public bool PlayTest(BetEssence bet)
		{
			int baseBetAmount = 10;
			int startCash = 5000;

			Bot bot = new BotDAlembert(bet, startCash);

			int firstCashAfterBet = bot.Play(1);

			bool isPlayCorrect = false;
			if ((firstCashAfterBet == startCash - baseBetAmount || firstCashAfterBet == startCash + bet.Coefficient * baseBetAmount))
			{
				isPlayCorrect = true;
			}

			return isPlayCorrect;
		}
	}
}
