using Roulette.Bets;
using Roulette.Cells;
using System;
using System.Collections.Generic;

namespace Roulette
{
	public class RouletteTable
	{
		public readonly Cell[] Numbers;
		public List<Player> Players { get; private set; }
		public List<Player> Observers { get; private set; }
		private List<Bet> bets;
		private Random rnd;

		public RouletteTable()
		{
			Numbers = new Cell[37];

			Numbers[0] = new Cell(0, ColourEnum.Zero, ParityEnum.Zero, DozenEnum.Zero);
			int a = 1;
			for (int x = 0; x < 2; x++)
			{
				for (int y = 0; y < 10; y++)
				{
					Numbers[a] = new Cell(a, (ColourEnum)(a % 2), (ParityEnum)(a % 2), (DozenEnum)(((a - 1) / 12) + 1));
					a++;
				}
				for (int z = 0; z < 8; z++)
				{
					Numbers[a] = new Cell(a, (ColourEnum)((a + 1) % 2), (ParityEnum)(a % 2), (DozenEnum)(((a - 1) / 12) + 1));
					a++;
				}
			}

			Players = new List<Player>();
			Observers = new List<Player>();
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
				int coefficient = bets[x].CheckBet(Numbers[number]);
				Players[bets[x].Player].AmountOfBets++;
				if (coefficient != 0)
					Players[bets[x].Player].BetsWin++;
				Players[bets[x].Player].Balance += bets[x].Money * coefficient;
				Players[bets[x].Player].Profit = Players[bets[x].Player].Balance - Players[bets[x].Player].Deposit;
			}
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