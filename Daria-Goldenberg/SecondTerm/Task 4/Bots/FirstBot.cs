using BlackJack;

namespace Bots
{
	public class FirstBot : Player
	{
		public FirstBot(Game game, int balance) : base(game, balance)
		{
			Name = "First Bot";
			Game = game;
			Balance = balance;
		}

		public override void MakeBet()
		{
			if (Balance < 50)
			{
				Bet = 5;
				Balance -= Bet;
			}
			else
			{
				Bet = 50;
				Balance -= Bet;
			}
		}
	}
}