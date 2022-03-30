namespace BlackJack
{
	public class Game
	{
		public Deck Deck;
		public List<Player> Players;
		public Croupier Croupier;

		public Game()
		{
			Deck = new Deck();
			Players = new List<Player>();
			Croupier = new Croupier(this);
		}

		public void Start()
		{
			Logger logger = new Logger(this);
			logger.WriteStartMessage();

			for (int i = 1; i <= 40; i++)
			{
				logger.WriteCurrentBet(i);

				if (Deck.Cards.Count < 140)
					Deck = new Deck();

				foreach (Player player in Players)
				{
					player.MakeBet();
					logger.WriteMadeBetMessage(player);
				}

				Croupier.BeginGame();

				foreach (Player player in Players)
				{
					for (int j = 0; j < 2; j++)
						player.Hit();
					logger.WriteReceivedCards(player);
				}

				foreach (Player player in Players)
					player.PlayTurn();

				while (Croupier.Hand.CountPoints() < 17)
					Croupier.Hand.TakeCard(Deck);
				logger.WriteCroupierCards();

				foreach (Player player in Players)
					player.Finish();

				for (int j = 0; j < Players.Count; j++)
					if (Players[j].Balance <= 0)
					{
						logger.WriteOutOfGameMessage(Players[j]);
						Players.Remove(Players[j]);
						j--;
					}

				if (Players.Count == 0)
				{
					logger.WriteGameOverMessage();
					break;
				}

				Croupier.Finish();
			}

			logger.WriteResults();
		}
	}
}
