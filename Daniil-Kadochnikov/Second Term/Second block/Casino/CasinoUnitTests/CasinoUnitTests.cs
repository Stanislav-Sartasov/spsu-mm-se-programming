using Bots;
using NUnit.Framework;
using Roulette;
using System.Collections.Generic;

namespace CasinoUnitTests
{
	public class Tests
	{
		private RouletteTable tableOne;

		[Test]
		public void InitializeRouletteTest()
		{
			tableOne = new RouletteTable();

			// Colour: 0 - black; 1 - red; 2 - zero;
			// Parity: 0 - even; 1 - odd; 2 - zero;
			// Dozen: 1 - dozen 1; 2 - dozen 2; 3 - dozen 3; 0 - zero;

			//0
			Assert.AreEqual(tableOne.Numbers[0].Colour, 2);
			Assert.AreEqual(tableOne.Numbers[0].Parity, 2);
			Assert.AreEqual(tableOne.Numbers[0].Dozen, 0);

			//3
			Assert.AreEqual(tableOne.Numbers[3].Colour, 1);
			Assert.AreEqual(tableOne.Numbers[3].Parity, 1);
			Assert.AreEqual(tableOne.Numbers[3].Dozen, 1);

			//18
			Assert.AreEqual(tableOne.Numbers[18].Colour, 1);
			Assert.AreEqual(tableOne.Numbers[18].Parity, 0);
			Assert.AreEqual(tableOne.Numbers[18].Dozen, 2);

			//29
			Assert.AreEqual(tableOne.Numbers[29].Colour, 0);
			Assert.AreEqual(tableOne.Numbers[29].Parity, 1);
			Assert.AreEqual(tableOne.Numbers[29].Dozen, 3);

			//36
			Assert.AreEqual(tableOne.Numbers[36].Colour, 1);
			Assert.AreEqual(tableOne.Numbers[36].Parity, 0);
			Assert.AreEqual(tableOne.Numbers[36].Dozen, 3);
		}

		[Test]
		public void InitializeBetTest()
		{
			Bet newBet = new Bet(1, "red", 100);
			Assert.AreEqual(newBet.Player, 1);
			Assert.AreEqual(newBet.BetCell, "red");
			Assert.AreEqual(newBet.Money, 100);
		}

		[Test]
		public void AddBotsTest()
		{
			tableOne = new RouletteTable();

			Player playerOne = new BotDAlembert("TestBotDAlembert", 27000);
			tableOne.AddPlayer(playerOne);
			Assert.AreEqual(tableOne.Players[0], playerOne);
			Assert.AreEqual(tableOne.Players[0].Deposit, 27000);
			playerOne.GetInfo();

			Player playerTwo = new BotLabouchere("TestBotLabouchere", 3000);
			tableOne.AddPlayer(playerTwo);
			Assert.AreEqual(tableOne.Players[1], playerTwo);
			Assert.AreEqual(tableOne.Players[1].Deposit, 3000);
			playerTwo.GetInfo();

			Player playerThree = new BotMartingale("TestBotMartingale", 7000);
			tableOne.AddPlayer(playerThree);
			Assert.AreEqual(tableOne.Players[2], playerThree);
			Assert.AreEqual(tableOne.Players[2].Deposit, 7000);
			playerThree.GetInfo();
		}

		[Test]
		public void SpinBotDAlembertTest()
		{
			tableOne = new RouletteTable();
			Player playerOne = new BotDAlembert("TestBotDAlembertOne", 10000);
			tableOne.AddPlayer(playerOne);


			tableOne.Spin();
			tableOne.GetInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].Name, "TestBotDAlembertOne");
			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 1);
			int oldBalance = tableOne.Players[0].Balance;

			tableOne.Spin();
			tableOne.GetInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 2);
			Assert.AreNotEqual(tableOne.Players[0].Balance, oldBalance);
		}

		[Test]
		public void SpinBotLabouchereTest()
		{
			tableOne = new RouletteTable();
			Player playerOne = new BotLabouchere("TestBotLabouchereOne", 20000);
			tableOne.AddPlayer(playerOne);

			tableOne.Spin();
			tableOne.GetInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].Name, "TestBotLabouchereOne");
			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 1);
			int oldBalance = tableOne.Players[0].Balance;

			tableOne.Spin();
			tableOne.GetInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 2);
			Assert.AreNotEqual(tableOne.Players[0].Balance, oldBalance);
		}

		[Test]
		public void SpinBotMartingaleTest()
		{
			tableOne = new RouletteTable();
			Player playerOne = new BotMartingale("TestBotMartingaleOne", 3000);
			tableOne.AddPlayer(playerOne);

			tableOne.Spin();
			tableOne.GetInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].Name, "TestBotMartingaleOne");
			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 1);
			int oldBalance = tableOne.Players[0].Balance;

			tableOne.Spin();
			tableOne.GetInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 2);
			Assert.AreNotEqual(tableOne.Players[0].Balance, oldBalance);
		}

		[Test]
		public void MakeBetBotDAlembertTest()
		{
			Player newBot = new BotDAlembert("BotDAlembert", 1000);
			List<Bet> newList = newBot.MakeBet(0);

			Assert.AreEqual(newList[0].Player, 0);
			Assert.AreEqual(newList[0].Money, newBot.Deposit - newBot.Balance);
			int unit = newBot.Deposit / 40;
			Assert.AreEqual(newList[0].Money, unit);
			Assert.AreEqual(newBot.Balance, newBot.Deposit - newList[0].Money);
			Assert.AreEqual(newBot.AmountOfBets, 1);
		}

		[Test]
		public void MakeBetBotLAbouchereTest()
		{
			Player newBot = new BotLabouchere("BotLabouchere", 2000);
			List<Bet> newList = newBot.MakeBet(0);

			int unit = newBot.Deposit / 20 / 15;
			Assert.AreEqual(newList[0].Money, newBot.Deposit / 20 - unit * 2 - unit * 3 - unit * 4);
			Assert.AreEqual(newBot.Balance, newBot.Deposit - newList[0].Money);
			Assert.AreEqual(newBot.AmountOfBets, 1);
		}

		[Test]
		public void MakeBetBotMartingaleTest()
		{
			Player newBot = new BotMartingale("BotMartingale", 3000);
			List<Bet> newList = newBot.MakeBet(0);

			Assert.AreEqual(newList[0].Money, newBot.Deposit / 40);
			Assert.AreEqual(newBot.Balance, newBot.Deposit - newList[0].Money);
			Assert.AreEqual(newBot.AmountOfBets, 1);
		}
	}
}