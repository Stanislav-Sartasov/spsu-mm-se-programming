using BlackJack;

namespace Bots
{
	public class ThirdBot : Player
	{
		public ThirdBot(Game game, int balance) : base(game, balance)
		{
			Name = "Third Bot";
			Game = game;
			Balance = balance;
		}

		public override void MakeBet()
		{
			if (800 <= Balance)
			{
				Bet = 120;
				Balance -= Bet;
			}
			else if (600 <= Balance && Balance < 800)
			{
				Bet = 60;
				Balance -= Bet;
			}
			else if (400 <= Balance && Balance < 600)
			{
				Bet = 30;
				Balance -= Bet;
			}
			else if (200 <= Balance && Balance < 400)
			{
				Bet = 15;
				Balance -= Bet;
			}
			else
			{
				Bet = 5;
				Balance -= Bet;
			}
		}
	}
}
