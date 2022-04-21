using System;
using System.Collections.Generic;
using System.Linq;

namespace Roulette
{
	public class RouletteTable
	{
		public readonly Cell[] Numbers;
		public List<Player> Players { get; private set; }
		public List<Player> Observers { get; private set; }
		private List<Bet> bets;
		private readonly string[] possibleBets;
		private Random rnd;

		public RouletteTable()
		{
			Numbers = new Cell[37];

			Numbers[0] = new Cell(2, 2, 0);
			int a = 1;
			for (int x = 0; x < 2; x++)
			{
				for (int y = 0; y < 10; y++)
				{
					Numbers[a] = new Cell(a % 2, a % 2, ((a - 1) / 12) + 1);
					a++;
				}
				for (int z = 0; z < 8; z++)
				{
					Numbers[a] = new Cell((a + 1) % 2, a % 2, ((a - 1) / 12) + 1);
					a++;
				}
			}

			Players = new List<Player>();
			Observers = new List<Player>();

			possibleBets = new string[44];
			for (int x = 0; x < 37; x++)
			{
				possibleBets[x] = x.ToString();
			}
			possibleBets[37] = "red";
			possibleBets[38] = "black";
			possibleBets[39] = "odd";
			possibleBets[40] = "even";
			possibleBets[41] = "dozen 1";
			possibleBets[42] = "dozen 2";
			possibleBets[43] = "dozen 3";

			rnd = new Random();
		}

		public void AddPlayer(Player newPlayer)
		{
			Players.Add(newPlayer);
		}

		public void Spin()
		{
			bets = new List<Bet>();
			for (int x = 0; x < Players.Count; x++)
			{

				List<Bet> playersBets = Players[x].MakeBet(x);

				if (Players[x].flag == 0)
				{
					Player playerObserver = Players[x];
					Players.RemoveAt(x);
					Observers.Add(playerObserver);
					x--;
				}

				if (playersBets == null)
					continue;

				foreach (Bet oneBet in playersBets)
					bets.Add(oneBet);
			}

			int number = rnd.Next(37);

			for (int x = 0; x < bets.Count; x++)
			{
				if (!(possibleBets.Contains(bets[x].BetCell)))
				{
					Players[bets[x].Player].Balance += bets[x].Money;
					Console.WriteLine("The player \"{0}\" made a non-existent bet on \"{1}\". Staked money \"{2}\" have been returned. Balance is \"{3}\".", Players[bets[x].Player].Name, bets[x].BetCell, bets[x].Money, Players[bets[x].Player].Balance);
					break;
				}

				Players[bets[x].Player].AmountOfBets++;
				try
				{
					int numberBet = Convert.ToInt32(bets[x].BetCell);

					if (numberBet == number)
						Win(36, x);
				}
				catch (FormatException)
				{
					if (Numbers[number].Colour == 0 && bets[x].BetCell == "black")
						Win(2, x);
					else if (Numbers[number].Colour == 1 && bets[x].BetCell == "red")
						Win(2, x);
					else if (Numbers[number].Parity == 0 && bets[x].BetCell == "even")
						Win(2, x);
					else if (Numbers[number].Parity == 1 && bets[x].BetCell == "odd")
						Win(2, x);
					else if (Numbers[number].Dozen == 1 && bets[x].BetCell == "dozen 1")
						Win(3, x);
					else if (Numbers[number].Dozen == 2 && bets[x].BetCell == "dozen 2")
						Win(3, x);
					else if (Numbers[number].Dozen == 3 && bets[x].BetCell == "dozen 3")
						Win(3, x);
				}
				Players[bets[x].Player].Profit = Players[bets[x].Player].Balance - Players[bets[x].Player].Deposit;
			}
		}

		private void Win(int coefficient, int x)
		{
			Players[bets[x].Player].Balance += bets[x].Money * coefficient;
			Players[bets[x].Player].BetsWin++;
		}

		public void ShowInfoAboutPlayers()
		{
			for (int x = 0; x < Players.Count; x++)
			{
				Players[x].ShowInfo();
			}
			for (int x = 0; x < Observers.Count; x++)
			{
				Observers[x].ShowInfo();
			}
		}
	}
}