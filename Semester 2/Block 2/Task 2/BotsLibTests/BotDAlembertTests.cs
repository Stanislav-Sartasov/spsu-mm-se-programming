using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BotsLib;

namespace BotsLibTests
{
	[TestClass]
	public class BotDAlembertTests
	{
		[TestMethod]
		public void WrongBetEssenceTest()
		{
			string betEssence = "37";
			bool isEssenceCorrect = true;
			Bot testBot = new BotDAlembert(10, betEssence, 5000);

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
			int baseBetAmount = 10;
			string betEssence = "red";
			int startCash = 5000;

			Bot testBot = new BotDAlembert(baseBetAmount, betEssence, startCash);

			int cashAfterBet = testBot.Play(1);
			bool isPlayCorrect = false;
			if (cashAfterBet == startCash - baseBetAmount || cashAfterBet == startCash + baseBetAmount)
			{
				isPlayCorrect = true; 
			}

			Assert.IsTrue(isPlayCorrect);
		}
	}
}
