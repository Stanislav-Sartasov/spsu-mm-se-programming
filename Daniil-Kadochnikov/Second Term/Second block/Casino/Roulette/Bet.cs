namespace Roulette
{
	public class Bet
	{
		public readonly int Player;
		public readonly string BetCell;
		public readonly int Money;

		public Bet(int player, string bet, int money)
		{
			this.Player = player;
			this.BetCell = bet;
			this.Money = money;
		}
	}
}