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

		public RouletteTable()
		{
			Numbers = new Cell[37];

			Numbers[0] = new Cell(2, 2, 0);
			byte a = 1;
			for (byte x = 0; x < 2; x++)
			{
				for (byte y = 0; y < 10; y++)
				{
					Numbers[a] = new Cell((byte)(a % 2), (byte)(a % 2), (byte)(((a - 1) / 12) + 1));
					a++;
				}
				for (byte z = 0; z < 8; z++)
				{
					Numbers[a] = new Cell((byte)((a + 1) % 2), (byte)(a % 2), (byte)(((a - 1) / 12) + 1));
					a++;
				}
			}

			Players = new List<Player>();
			Observers = new List<Player>();
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
				if (Players[x].flag == 0)
				{
					Player playerObserver = Players[x];
					Players.RemoveAt(x);
					Observers.Add(playerObserver);
					x--;
				}
				else
				{
					List<Bet> playersBets = Players[x].MakeBet(x);

					if (playersBets == null)
						continue;

					foreach (Bet oneBet in playersBets)
						bets.Add(oneBet);
				}
			}

			Random rnd = new Random();
			int number = rnd.Next(0, 37);

			List<string> wins = new List<string>();

			wins.Add(rnd.ToString());

			if (Numbers[number].Colour == 0)
				wins.Add("black");
			else if (Numbers[number].Colour == 1)
				wins.Add("red");

			if (Numbers[number].Parity == 0)
				wins.Add("even");
			else if (Numbers[number].Parity == 1)
				wins.Add("odd");

			if (Numbers[number].Dozen == 1)
				wins.Add("dozen 1");
			else if (Numbers[number].Dozen == 2)
				wins.Add("dozen 2");
			else if (Numbers[number].Dozen == 3)
				wins.Add("dozen 3");

			for (int x = 0; x < bets.Count; x++)
			{
				if (wins.Contains(bets[x].BetCell))
				{
					if (bets[x].BetCell == wins[0])
						Players[bets[x].Player].Balance += bets[x].Money * 36;
					else if (bets[x].BetCell == wins[1] || bets[x].BetCell == wins[2])
						Players[bets[x].Player].Balance += bets[x].Money * 2;
					else
						Players[bets[x].Player].Balance += bets[x].Money * 3;

					Players[bets[x].Player].BetsWin++;
				}
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
