using Bots;
using NUnit.Framework;
using Plugin;
using Roulette;
using Roulette.Bets;
using Roulette.Cells;
using System;
using System.Collections.Generic;

namespace CasinoUnitTests
{
	public class Tests
	{
		[Test]
		public void FindBotTest()
		{
			List<Type> bots = new BotLoader().FindBots("../../../Dll", typeof(Bot));
			Assert.AreEqual(bots.Count, 3);
			Assert.AreEqual(bots[0], typeof(BotDAlembert));
			Assert.AreEqual(bots[1], typeof(BotLabouchere));
			Assert.AreEqual(bots[2], typeof(BotMartingale));
		}

		[Test]
		public void InitializeCellTest()
		{
			Cell newCell = new Cell(1, ColourEnum.Red, ParityEnum.Even, DozenEnum.DozenOne);
			Assert.AreEqual(newCell.Colour, ColourEnum.Red);
			Assert.AreEqual(newCell.Parity, ParityEnum.Even);
			Assert.AreEqual(newCell.Dozen, DozenEnum.DozenOne);
		}

		[Test]
		public void InitializeBetTest()
		{
			Cell cellOne = new Cell(1, ColourEnum.Black, ParityEnum.Odd, DozenEnum.DozenTwo);
			Cell cellTwo = new Cell(23, ColourEnum.Red, ParityEnum.Even, DozenEnum.DozenThree);

			ColourBet betOne = new ColourBet(1, 1000, ColourEnum.Red);
			Assert.AreEqual(betOne.Player, 1);
			Assert.AreEqual(betOne.Money, 1000);
			Assert.AreEqual(betOne.BetCell, ColourEnum.Red);
			Assert.AreEqual(betOne.CheckBet(cellOne), 0);
			Assert.AreEqual(betOne.CheckBet(cellTwo), 2);

			DozenBet betTwo = new DozenBet(1241411, 132420, DozenEnum.DozenThree);
			Assert.AreEqual(betTwo.Player, 1241411);
			Assert.AreEqual(betTwo.Money, 132420);
			Assert.AreEqual(betTwo.BetCell, DozenEnum.DozenThree);
			Assert.AreEqual(betTwo.CheckBet(cellOne), 0);
			Assert.AreEqual(betTwo.CheckBet(cellTwo), 3);

			ParityBet betThree = new ParityBet(0, 12, ParityEnum.Even);
			Assert.AreEqual(betThree.Player, 0);
			Assert.AreEqual(betThree.Money, 12);
			Assert.AreEqual(betThree.BetCell, ParityEnum.Even);
			Assert.AreEqual(betThree.CheckBet(cellOne), 0);
			Assert.AreEqual(betThree.CheckBet(cellTwo), 2);

			NumberBet betFour = new NumberBet(2, 1200000, 23);
			Assert.AreEqual(betFour.Player, 2);
			Assert.AreEqual(betFour.Money, 1200000);
			Assert.AreEqual(betFour.BetCell, 23);
			Assert.AreEqual(betFour.CheckBet(cellOne), 0);
			Assert.AreEqual(betFour.CheckBet(cellTwo), 36);

			Assert.Throws<System.ArgumentOutOfRangeException>(() => new NumberBet(34, 123456, 46));
		}

		[Test]
		public void InitializeRouletteTest()
		{
			RouletteTable tableOne = new RouletteTable();

			Assert.AreEqual(tableOne.Numbers[0].Colour, null);
			Assert.AreEqual(tableOne.Numbers[0].Parity, null);
			Assert.AreEqual(tableOne.Numbers[0].Dozen, null);

			Assert.AreEqual(tableOne.Numbers[3].Colour, ColourEnum.Red);
			Assert.AreEqual(tableOne.Numbers[3].Parity, ParityEnum.Odd);
			Assert.AreEqual(tableOne.Numbers[3].Dozen, DozenEnum.DozenOne);

			Assert.AreEqual(tableOne.Numbers[18].Colour, ColourEnum.Red);
			Assert.AreEqual(tableOne.Numbers[18].Parity, ParityEnum.Even);
			Assert.AreEqual(tableOne.Numbers[18].Dozen, DozenEnum.DozenTwo);

			Assert.AreEqual(tableOne.Numbers[29].Colour, ColourEnum.Black);
			Assert.AreEqual(tableOne.Numbers[29].Parity, ParityEnum.Odd);
			Assert.AreEqual(tableOne.Numbers[29].Dozen, DozenEnum.DozenThree);

			Assert.AreEqual(tableOne.Numbers[36].Colour, ColourEnum.Red);
			Assert.AreEqual(tableOne.Numbers[36].Parity, ParityEnum.Even);
			Assert.AreEqual(tableOne.Numbers[36].Dozen, DozenEnum.DozenThree);

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