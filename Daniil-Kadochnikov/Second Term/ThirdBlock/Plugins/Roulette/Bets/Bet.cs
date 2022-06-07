using Roulette.Cells;

namespace Roulette.Bets
{
	public abstract class Bet
	{
		public readonly int Player;
		public readonly int Money;

		public Bet(int player, int money)
		{
			Player = player;
			Money = money;
		}

		public abstract int CheckBet(Cell winCell);
	}
}