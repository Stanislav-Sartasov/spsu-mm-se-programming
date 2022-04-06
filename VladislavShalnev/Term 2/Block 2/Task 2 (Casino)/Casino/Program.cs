using Blackjack;
using Blackjack.Cards;
using Blackjack.Players;
using Bots;
using Utils;

namespace Casino;

public class Program
{
	public static void Main(string[] args)
	{
		Logger.Success("Welcome to a Blackjack game!\n");
		Logger.Log("All bots have 1000$");

		Game game = new Game();

		List<Player> bots = new List<Player>()
		{
			new SillyBot("Jeremy", 1000),
			new MadBot("William", 1000),
			new RiskyBot("Elizabeth", 1000)
		};

		foreach (Player bot in bots)
			game.Players.Add(bot);

		SubscribeToEvents(game);
		
		for (int i = 0; i < 40; i++)
			game.Start();
		
		Logger.Success("\n--Results--");
		foreach (Player bot in bots)
			Logger.Log($"{bot.Name} has {bot.Money}$");
	}

	public static void SubscribeToEvents(Game game)
	{
		game.OnStart += () =>
			Logger.Success("\n--Bets--");

		game.OnEnd += () =>
		{
			Logger.Log("");

			var printCards = (BasePlayer player) =>
			{
				foreach (Card card in player.Cards)
					Logger.Log($"{card}, ", end: "");
				Logger.Log("total ", end: "");
				if (player.Score > 21)
					Logger.Error(player.Score.ToString(), " ");
				else if (player.Score == 21)
					Logger.Success(player.Score.ToString(), " ");
				else
					Logger.Log(player.Score.ToString(), end: " ");
				Logger.Log("points");
			};

			foreach (Player player in game.Players)
			{
				Logger.Log($"{player.Name} has ", end: "");
				printCards(player);
			}

			Logger.Log("Dealer has ", end: "");
			printCards(game.Dealer);
			
			Logger.Log("");
		};

		game.Dealer.OnBlackjack += () =>
			Logger.Log("Dealer got a blackjack!");

		foreach (Player player in game.Players)
		{
			player.OnBet += () =>
				Logger.Log($"{player.Name} bet {player.Bet}$");
			
			player.OnBlackjack += () =>
				Logger.Log($"{player.Name} got a blackjack and now has {player.Money}$!");
			
			player.OnWin += () =>
				Logger.Log($"{player.Name} won and now has {player.Money}$");
			
			player.OnLoss += () =>
				Logger.Log($"{player.Name} lost and now has {player.Money}$");
			
			player.OnTie += () =>
				Logger.Log($"{player.Name} got a tie and still has {player.Money}$");
			
			player.OnKick += () =>
				Logger.Log($"{player.Name} left penniless and was kicked out");
			
			player.OnHit += () =>
				Logger.Log($"{player.Name} takes card");
			
			player.OnStand += () =>
				Logger.Log($"{player.Name} stands");
		}
	}
}