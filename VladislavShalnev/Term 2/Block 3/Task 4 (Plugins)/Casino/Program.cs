using Blackjack;
using Blackjack.Cards;
using Blackjack.Players;
using Utils;
using Loaders;

namespace Casino;

public class Program
{
	public static void Main(string[] args)
	{
		try
		{
			Logger.Log("This program loads bots from given directory path and plays Blackjack with them.");
			
			if (args.Length != 1)
				throw new ArgumentException("Not enough or too much command line parameters.");
			
			// There are some test dlls in Loaders.UnitTests/Dlls
			List<Player> bots = BotLoader.Load(args[0]);
			bots.ForEach(bot => bot.Money = 1000);
			
			Logger.Log($"Loaded {bots.Count} bots from \"{args[0]}\".\n");
			if (bots.Count == 0)
				return;

			Logger.Success("Welcome to a Blackjack game!\n");
			Logger.Log("All bots have 1000$");

			Game game = new Game();

			foreach (Player bot in bots)
				game.Players.Add(bot);

			SubscribeToEvents(game);
		
			for (int i = 0; i < 40; i++)
				game.Start();
		
			Logger.Success("\n--Results--");
			foreach (Player bot in bots)
				Logger.Log($"{bot.Name} has {bot.Money}$");
		}
		catch (ArgumentException ex)
		{
			Logger.Error(ex.Message);
		}
		catch (IOException ex)
		{
			Logger.Error(ex.Message);
		}
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
				Logger.Log($"{player.Name} got a blackjack!");
			
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