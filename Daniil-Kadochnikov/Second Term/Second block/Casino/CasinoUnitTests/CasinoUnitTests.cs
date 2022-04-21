using Bots;
using NUnit.Framework;
using Roulette;
using System.Collections.Generic;

namespace CasinoUnitTests
{
	public class Tests
	{
		[Test]
		public void InitializeBetTest()
		{
			Bet newBet = new Bet(1, "red", 100);
			Assert.AreEqual(newBet.Player, 1);
			Assert.AreEqual(newBet.BetCell, "red");
			Assert.AreEqual(newBet.Money, 100);
		}

		[Test]
		public void InitializeCellTest()
		{
			Cell newCell = new Cell(1, 0, 1);
			Assert.AreEqual(newCell.Colour, 1);
			Assert.AreEqual(newCell.Parity, 0);
			Assert.AreEqual(newCell.Dozen, 1);
		}

		[Test]
		public void InitializeRouletteTest()
		{
			RouletteTable tableOne = new RouletteTable();
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

			Assert.AreEqual(tableOne.Players.Count, 0);
			Assert.AreEqual(tableOne.Observers.Count, 0);
		}

		[Test]
		public void AddBotsTest()
		{
			RouletteTable tableOne = new RouletteTable();

			Player playerOne = new BotDAlembert("TestBotDAlembert", 27000);
			tableOne.AddPlayer(playerOne);
			Assert.AreEqual(tableOne.Players.Count, 1);
			Assert.AreEqual(tableOne.Players[0], playerOne);
			Assert.AreEqual(tableOne.Players[0].Deposit, 27000);
			playerOne.ShowInfo();

			Player playerTwo = new BotLabouchere("TestBotLabouchere", 3000);
			tableOne.AddPlayer(playerTwo);
			Assert.AreEqual(tableOne.Players.Count, 2);
			Assert.AreEqual(tableOne.Players[1], playerTwo);
			Assert.AreEqual(tableOne.Players[1].Deposit, 3000);
			playerTwo.ShowInfo();

			Player playerThree = new BotMartingale("TestBotMartingale", 7000);
			tableOne.AddPlayer(playerThree);
			Assert.AreEqual(tableOne.Players.Count, 3);
			Assert.AreEqual(tableOne.Players[2], playerThree);
			Assert.AreEqual(tableOne.Players[2].Deposit, 7000);
			playerThree.ShowInfo();
		}

		[Test]
		public void SpinBotDAlembertTest()
		{
			RouletteTable tableOne = new RouletteTable();
			Player playerOne = new BotDAlembert("TestBotDAlembertOne", 0);
			tableOne.AddPlayer(playerOne);
			tableOne.Spin();
			Assert.AreEqual(tableOne.Observers[0], playerOne);

			Player playerTwo = new BotDAlembert("TestBotDAlembertTwo", 10000);
			tableOne.AddPlayer(playerTwo);

			tableOne.Spin();
			tableOne.ShowInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].Name, "TestBotDAlembertTwo");
			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 1);
			int oldBalance = tableOne.Players[0].Balance;

			tableOne.Spin();
			tableOne.ShowInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 2);
			Assert.AreNotEqual(tableOne.Players[0].Balance, oldBalance);
		}

		[Test]
		public void SpinBotLabouchereTest()
		{
			RouletteTable tableOne = new RouletteTable();
			Player playerOne = new BotDAlembert("TestBotLabouchereOne", 0);
			tableOne.AddPlayer(playerOne);
			tableOne.Spin();
			Assert.AreEqual(tableOne.Observers[0], playerOne);

			Player playerTwo = new BotLabouchere("TestBotLabouchereTwo", 20000);
			tableOne.AddPlayer(playerTwo);

			tableOne.Spin();
			tableOne.ShowInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].Name, "TestBotLabouchereTwo");
			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 1);
			int oldBalance = tableOne.Players[0].Balance;

			tableOne.Spin();
			tableOne.ShowInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 2);
			Assert.AreNotEqual(tableOne.Players[0].Balance, oldBalance);
		}

		[Test]
		public void SpinBotMartingaleTest()
		{
			RouletteTable tableOne = new RouletteTable();
			Player playerOne = new BotDAlembert("TestBotMartingaleOne", 0);
			tableOne.AddPlayer(playerOne);
			tableOne.Spin();
			Assert.AreEqual(tableOne.Observers[0], playerOne);

			Player playerTwo = new BotMartingale("TestBotMartingaleTwo", 3000);
			tableOne.AddPlayer(playerTwo);

			tableOne.Spin();
			tableOne.ShowInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].Name, "TestBotMartingaleTwo");
			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 1);
			int oldBalance = tableOne.Players[0].Balance;

			tableOne.Spin();
			tableOne.ShowInfoAboutPlayers();

			Assert.AreEqual(tableOne.Players[0].AmountOfBets, 2);
			Assert.AreNotEqual(tableOne.Players[0].Balance, oldBalance);
		}

		[Test]
		public void MakeBetBotDAlembertTest()
		{
			RouletteTable tableOne = new RouletteTable();

			Player newBot = new BotDAlembert("TestBotDAlembertOne", 0);
			List<Bet> newList = newBot.MakeBet(0);
			Assert.AreEqual(newList, null);

			Player playerTwo = new BotDAlembert("TestBotDAlembertTwo", 10000);
			tableOne.AddPlayer(playerTwo);
			int bets = 0;

			newList = tableOne.Players[0].MakeBet(0);
			Assert.AreEqual(newList[0].Player, 0);
			int unit = playerTwo.Deposit / 40;
			Assert.AreEqual(newList[0].Money, unit);
			Assert.AreEqual(playerTwo.AmountOfBets, 0);

			while (tableOne.Observers.Count == 0)
			{
				tableOne.Spin();
				if (tableOne.Observers.Count == 0)
					bets++;
				Assert.AreEqual(playerTwo.AmountOfBets, bets);
				Assert.AreEqual(playerTwo.Profit, playerTwo.Balance - 10000);
			}
			playerTwo.ShowInfo();
		}

		[Test]
		public void MakeBetBotLAbouchereTest()
		{
			RouletteTable tableOne = new RouletteTable();

			Player newBot = new BotLabouchere("TestBotLabouchereOne", 0);
			List<Bet> newList = newBot.MakeBet(0);
			Assert.AreEqual(newList, null);

			Player playerTwo = new BotLabouchere("TestBotLabouchereTwo", 20000);
			tableOne.AddPlayer(playerTwo);
			int bets = 0;

			newList = tableOne.Players[0].MakeBet(0);
			Assert.AreEqual(newList[0].Player, 0);
			int unit = playerTwo.Deposit / 20 / 15;
			Assert.AreEqual(newList[0].Money, playerTwo.Deposit / 20 - unit * 2 - unit * 3 - unit * 4);
			Assert.AreEqual(playerTwo.AmountOfBets, 0);

			while (tableOne.Observers.Count == 0)
			{
				tableOne.Spin();
				if (tableOne.Observers.Count == 0)
					bets++;
				Assert.AreEqual(playerTwo.AmountOfBets, bets);
				Assert.AreEqual(playerTwo.Profit, playerTwo.Balance - 20000);
			}
			playerTwo.ShowInfo();
		}

		[Test]
		public void MakeBetBotMartingaleTest()
		{
			RouletteTable tableOne = new RouletteTable();

			Player newBot = new BotMartingale("TestBotMartingaleOne", 0);
			List<Bet> newList = newBot.MakeBet(0);
			Assert.AreEqual(newList, null);

			Player playerTwo = new BotMartingale("TestBotMartingaleTwo", 15000);
			tableOne.AddPlayer(playerTwo);
			int bets = 0;

			newList = tableOne.Players[0].MakeBet(0);
			Assert.AreEqual(newList[0].Player, 0);
			int unit = playerTwo.Deposit / 40;
			Assert.AreEqual(newList[0].Money, unit);
			Assert.AreEqual(playerTwo.AmountOfBets, 0);

			while (tableOne.Observers.Count == 0)
			{
				tableOne.Spin();
				if (tableOne.Observers.Count == 0)
					bets++;
				Assert.AreEqual(playerTwo.AmountOfBets, bets);
				Assert.AreEqual(playerTwo.Profit, playerTwo.Balance - 15000);
			}
			playerTwo.ShowInfo();
		}
	}
}