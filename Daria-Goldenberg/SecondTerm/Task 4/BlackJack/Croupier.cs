namespace BlackJack
{
	public class Croupier
	{
		public Hand Hand { get; private set; }
		private readonly Game game;

		public Croupier(Game game)
		{
			this.game = game;
			Hand = new Hand();
		}

		public void BeginGame()
		{
			Logger logger = new Logger(this);
			for (int i = 0; i < 2; i++)
				Hand.TakeCard(game.Deck);

			logger.WriteFirstCroupierCard();
		}

		public void DrawCards()
		{
			Logger logger = new Logger(this);
			while (Hand.CountPoints() < 17)
				Hand.TakeCard(game.Deck);

			logger.WriteCroupierCards(this);
		}

		public void Finish()
		{
			Hand.Clear();
		}

		public bool IsBlackJack()
		{
			return Hand.CountPoints() == 21 && Hand.Cards.Count == 2;
		}
	}
}