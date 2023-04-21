using BlackJack;

namespace Bots
{
	public class SecondBot : Player
	{
		public SecondBot(Game game, int balance) : base(game, balance)
		{
			Name = "Second Bot";
			Game = game;
			Balance = balance;
		}

		public override void MakeBet()
		{
			if (Balance % 16 == 0)
			{
				Bet = Balance / 16;
				Balance -= Bet;
			}
			else if (Balance % 8 == 0)
			{
				Bet = Balance / 8;
				Balance -= Bet;
			}
			else if (Balance % 4 == 0)
			{
				Bet = Balance / 4;
				Balance -= Bet;
			}
			else if (Balance % 2 == 0)
			{
				Bet = Balance / 2;
				Balance -= Bet;
			}
			else
			{
				Bet = 10;
				Balance -= Bet;
			}
		}
	}
}
