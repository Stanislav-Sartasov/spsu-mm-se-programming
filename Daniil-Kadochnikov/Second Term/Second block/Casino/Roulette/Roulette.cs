using System;
using System.Collections.Generic;

namespace Roulette
{
	public class RouletteTable
	{
		public readonly Cell[] Numbers;
		public List<Player> Players { get; private set; }
		public List<Player> Observers { get; private set; }
		private List<Bet> Bets;

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
			Bets = new List<Bet>();
			for (int x = 0; x < Players.Count; x++)
			{
				if (Players[x].Flag == 0)
				{
					Player playerObserver = Players[x];
					Players.RemoveAt(x);
					Observers.Add(playerObserver);
					continue;
				}

				List<Bet> playersBets = Players[x].MakeBet(x);

				if (playersBets == null)
					continue;

				foreach (Bet oneBet in playersBets)
					Bets.Add(oneBet);
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

			for (int x = 0; x < Bets.Count; x++)
			{
				if (wins.Contains(Bets[x].BetCell))
				{
					if (Bets[x].BetCell == wins[0])
						Players[Bets[x].Player].Balance += Bets[x].Money * 36;
					else if (Bets[x].BetCell == wins[1] || Bets[x].BetCell == wins[2])
						Players[Bets[x].Player].Balance += Bets[x].Money * 2;
					else
						Players[Bets[x].Player].Balance += Bets[x].Money * 3;

					Players[Bets[x].Player].betsWin++;
				}
				Players[Bets[x].Player].profit = Players[Bets[x].Player].Balance - Players[Bets[x].Player].Deposit;
			}
		}

		public void GetInfoAboutPlayers()
		{
			for (int x = 0; x < Players.Count; x++)
			{
				Players[x].GetInfo();
			}
			for (int x = 0; x < Observers.Count; x++)
			{
				Observers[x].GetInfo();
			}
		}
	}
}
