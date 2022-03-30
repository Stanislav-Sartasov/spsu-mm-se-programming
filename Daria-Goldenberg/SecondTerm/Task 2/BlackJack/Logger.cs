namespace BlackJack
{
	public class Logger
	{
		private readonly Game Game;
		private readonly Player Player;
		private readonly Croupier Croupier;

		public Logger(Game game)
		{
			Game = game;
		}

		public Logger(Game game, Player player)
		{
			Game = game;
			Player = player;
		}

		public Logger(Croupier croupier)
		{
			Croupier = croupier;
		}

		public void WriteStartMessage()
		{
			Console.WriteLine("Three bots start playing blackjack.");
			for (int i = 0; i < Game.Players.Count; i++)
				Console.WriteLine(Game.Players[i].Name + " has " + Game.Players[i].Balance + "$.");
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
				Console.Write(player.Hand.Cards[j].Name + " ");
			Console.WriteLine("(" + player.Hand.CountPoints() + " points)\n");
		}

		public void WriteCroupierCards()
		{
			Console.Write("\nDealer has the cards: ");
			foreach (Card card in Game.Croupier.Hand.Cards)
				Console.Write(card.Name + " ");
			Console.WriteLine("(" + Game.Croupier.Hand.CountPoints() + " points)\n");
		}

		public void WriteOutOfGameMessage(Player player)
		{
			Console.WriteLine(player.Name + " doesn't have enough money. He's out of game.");
		}

		public void WriteResults()
		{
			Console.WriteLine("---Results---");
			foreach (Player player in Game.Players)
				Console.WriteLine(player.Name + " has " + player.Balance + "$");
			if (Game.Players.Count != 3)
				Console.WriteLine("Other players are out of game, because they spent their money.");
		}

		public void WriteTakeCardMessage()
		{
			Console.WriteLine(Player.Name + " takes card.");
		}

		public void WriteStandMessage()
		{
			Console.WriteLine(Player.Name + " decides to stand.");
		}

		public void WriteHandCards()
		{
			Console.Write(Player.Name + " has the cards: ");
			foreach (Card card in Player.Hand.Cards)
				Console.Write(card.Name + " ");
			Console.WriteLine("(" + Player.Hand.CountPoints() + " points)");
		}

		public void WriteBlackJackMessage()
		{
			Console.WriteLine(Player.Name + " has blackjack and won.");
		}

		public void WriteFinishMessage()
		{
			int pointsOfCroupier = Game.Croupier.Hand.CountPoints();
			int pointsOfPlayer = Player.Hand.CountPoints();

			if (pointsOfPlayer > 21)
				Console.WriteLine(Player.Name + " got too much and lost.");
			else if (pointsOfPlayer == 21)
				if (pointsOfCroupier == 21 && Game.Croupier.Hand.Cards.Count == 2)
					Console.WriteLine(Player.Name + " has 21 points, but it's push.");
				else
					Console.WriteLine(Player.Name + " has 21 points and won.");
			else
			{
				if (pointsOfCroupier > pointsOfPlayer && pointsOfCroupier <= 21)
					Console.WriteLine(Player.Name + " has few points and lost.");
				else if (pointsOfCroupier > 21 || pointsOfCroupier < pointsOfPlayer)
					Console.WriteLine(Player.Name + " has " + Player.Hand.CountPoints() + " points and won.");
				else if (pointsOfCroupier == pointsOfPlayer)
					Console.WriteLine(Player.Name + " got push.");
			}
		}

		public void WriteBalance()
		{
			Console.WriteLine("Now " + Player.Name + " has " + Player.Balance + "$\n");
		}

		public void WriteFirstCroupierCard()
		{
			Console.WriteLine("\nDealer's first card is " + Croupier.Hand.Cards[0].Name + "\n");
		}
	}
}