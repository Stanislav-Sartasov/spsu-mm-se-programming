namespace BlackJack
{
	public class Croupier
	{
		public Hand Hand;
		private readonly Game Game;

		public Croupier(Game game)
		{
			Game = game;
			Hand = new Hand();
		}

		public void BeginGame()
		{
			Logger logger = new Logger(this);
			for (int i = 0; i < 2; i++)
				Hand.TakeCard(Game.Deck);

			logger.WriteFirstCroupierCard();
		}

		public void Finish()
		{
			Hand.Clear();
		}

		public bool IsBlackJack()
		{
			if (Hand.CountPoints() == 21 && Hand.Cards.Count == 2)
				return true;
			return false;
		}
	}
}
