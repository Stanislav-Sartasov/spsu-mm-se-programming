namespace BlackJack
{
	public abstract class Player
	{
		public string Name { get; protected set; }
		public Hand Hand { get; protected set; }
		protected Game Game;
		public int Balance { get; protected set; }
		public int Bet { get; protected set; }

		public Player(Game game, int balance)
		{
			Game = game;
			Hand = new Hand();
			Balance = balance;
			Bet = 0;
		}

		public abstract void MakeBet();

		public void BeginGame()
		{
			Logger logger = new Logger(Game, this);
			for (int j = 0; j < 2; j++)
				Hit();

			logger.WriteReceivedCards(this);
		}

		public void PlayTurn()
		{
			Logger logger = new Logger(Game, this);
			if (IsBlackJack())
				logger.WriteBlackJackMessage();
			else
				while (Hand.CountPoints() < 21)
					if (Hand.CountPoints() > 18)
					{
						logger.WriteStandMessage();
						break;
					}
					else
					{
						Hit();
						logger.WriteTakeCardMessage();
					}
			logger.WriteHandCards();
		}

		public void Hit()
		{
			Hand.TakeCard(Game.Deck);
		}

		public bool IsBlackJack()
		{
			return Hand.CountPoints() == 21 && Hand.Cards.Count == 2;
		}

		public void Finish()
		{
			Logger logger = new Logger(Game, this);
			int pointsOfCroupier = Game.Croupier.Hand.CountPoints();
			int pointsOfPlayer = Hand.CountPoints();

			if (IsBlackJack())
			{
				if (Game.Croupier.IsBlackJack())
					Balance += Bet;
				else
					Balance += (int)(1.5 * Bet);
			}
			else if (pointsOfPlayer == 21 && Hand.Cards.Count != 2)
			{
				if (pointsOfCroupier == 21)
					Balance += Bet;
				else
					Balance += (int)(1.5 * Bet);
			}
			else if (pointsOfPlayer < 21)
			{
				if (pointsOfPlayer == pointsOfCroupier)
					Balance += Bet;
				else if (pointsOfCroupier > 21 || pointsOfCroupier < pointsOfPlayer)
					Balance += (int)(1.5 * Bet);
			}

			logger.WriteFinishMessage();
			logger.WriteBalance();
			Bet = 0;
			Hand.Clear();
		}
	}
}
