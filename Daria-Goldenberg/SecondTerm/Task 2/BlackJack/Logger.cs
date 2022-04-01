namespace BlackJack
{
	public class Logger
	{
		private readonly Game game;
		private readonly Player player;
		private readonly Croupier croupier;

		public Logger(Game game)
		{
			this.game = game;
		}

		public Logger(Game game, Player player)
		{
			this.game = game;
			this.player = player;
		}

		public Logger(Croupier croupier)
		{
			this.croupier = croupier;
		}

		public void WriteStartMessage()
		{
			Console.WriteLine("Three bots start playing blackjack.");
			for (int i = 0; i < game.Players.Count; i++)
				Console.WriteLine(game.Players[i].Name + " has " + game.Players[i].Balance + "$.");
			Console.WriteLine();
		}

		public void WriteCurrentBet(int bet)
		{
			Console.WriteLine("---This is " + bet + " bet.\n");
		}

		public void WriteGameOverMessage()
		{
			Console.WriteLine("There are no more players in the game, who can bet. The game is over.");
		}

		public void WriteMadeBetMessage(Player player)
		{
			Console.WriteLine(player.Name + " made a bet of " + player.Bet + "$");
		}

		public void WriteReceivedCards(Player player)
		{
			Console.Write(player.Name + " receives cards: ");
			for (int j = 0; j < 2; j++)
				Console.Write(player.Hand.Cards[j].GetName() + " ");
			Console.WriteLine("(" + player.Hand.CountPoints() + " points)\n");
		}

		public void WriteCroupierCards()
		{
			Console.Write("\nDealer has the cards: ");
			foreach (Card card in game.Croupier.Hand.Cards)
				Console.Write(card.GetName() + " ");
			Console.WriteLine("(" + game.Croupier.Hand.CountPoints() + " points)\n");
		}

		public void WriteOutOfGameMessage(Player player)
		{
			Console.WriteLine(player.Name + " doesn't have enough money. He's out of game.\n");
		}

		public void WriteResults()
		{
			Console.WriteLine("---Results---");
			foreach (Player player in game.Players)
				Console.WriteLine(player.Name + " has " + player.Balance + "$");
			if (game.Players.Count != 3)
				Console.WriteLine("Other players are out of game, because they spent their money.");
		}

		public void WriteTakeCardMessage()
		{
			Console.WriteLine(player.Name + " takes card.");
		}

		public void WriteStandMessage()
		{
			Console.WriteLine(player.Name + " decides to stand.");
		}

		public void WriteHandCards()
		{
			Console.Write(player.Name + " has the cards: ");
			foreach (Card card in player.Hand.Cards)
				Console.Write(card.GetName() + " ");
			Console.WriteLine("(" + player.Hand.CountPoints() + " points)");
		}

		public void WriteBlackJackMessage()
		{
			Console.WriteLine(player.Name + " has blackjack and won.");
		}

		public void WriteFinishMessage()
		{
			int pointsOfCroupier = game.Croupier.Hand.CountPoints();
			int pointsOfPlayer = player.Hand.CountPoints();

			if (pointsOfPlayer > 21)
				Console.WriteLine(player.Name + " got too much and lost.");
			else if (pointsOfPlayer == 21)
				if (pointsOfCroupier == 21 && game.Croupier.Hand.Cards.Count == 2)
					Console.WriteLine(player.Name + " has 21 points, but it's push.");
				else
					Console.WriteLine(player.Name + " has 21 points and won.");
			else
			{
				if (pointsOfCroupier > pointsOfPlayer && pointsOfCroupier <= 21)
					Console.WriteLine(player.Name + " has few points and lost.");
				else if (pointsOfCroupier > 21 || pointsOfCroupier < pointsOfPlayer)
					Console.WriteLine(player.Name + " has " + player.Hand.CountPoints() + " points and won.");
				else if (pointsOfCroupier == pointsOfPlayer)
					Console.WriteLine(player.Name + " got push.");
			}
		}

		public void WriteBalance()
		{
			Console.WriteLine("Now " + player.Name + " has " + player.Balance + "$\n");
		}

		public void WriteFirstCroupierCard()
		{
			Console.WriteLine("\nDealer's first card is " + croupier.Hand.Cards[0].GetName() + "\n");
		}
	}
}