using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BotsLib;

namespace BotsLibTests
{
	[TestClass]
	public class BotLabouchererTests
	{
		[TestMethod]
		public void WrongBetEssenceTest()
		{
			string betEssence = "37";
			bool isEssenceCorrect = true;
			Bot testBot = new BotLaboucherer(betEssence, 5000);

			try
			{
				testBot.Play(40);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("The bet can only be on: white or black, even or odd, first/second/third dozen, a number from [0,36]"))
				{
					isEssenceCorrect = false;
				}
			}

			Assert.IsFalse(isEssenceCorrect);
		}

		[TestMethod]
		public void PlayTest()
		{
			string betEssence = "red";
			int startCash = 5000;
			Bot testBot = new BotLaboucherer(betEssence, startCash);

			int cashInPlay = (int)(0.1 * startCash);
			int sequenceMember = (int)(cashInPlay / 5);

			int cashAfterBet = testBot.Play(1);
			int cashAfterLoss = startCash - (sequenceMember + cashInPlay - (5 - 1) * sequenceMember);
			int cashAfterWin = startCash + (sequenceMember + cashInPlay - (5 - 1) * sequenceMember);
			bool isPlayCorrect = false;
			if (cashAfterBet == cashAfterLoss || cashAfterBet == cashAfterWin)
			{
				isPlayCorrect = true;
			}

			Assert.IsTrue(isPlayCorrect);
		}
	}
}
