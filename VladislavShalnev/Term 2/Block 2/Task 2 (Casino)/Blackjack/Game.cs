using Blackjack.Cards;
using Blackjack.Players;

namespace Blackjack;

public class Game
{
	public List<Player> Players;
	public readonly Dealer Dealer;
	private Deck _deck;
	
	// Game events
	public Action? OnStart;
	public Action? OnEnd;
	
	public Game()
	{
		Players = new List<Player>();
		Dealer = new Dealer();
	}

	public void Start()
	{
		OnStart?.Invoke();
		
		_deck = new Deck();

		// Clearing cards
		Dealer.Clear();
		foreach (Player player in Players)
			player.Clear();
		
		// Making bets
		foreach (Player player in Players)
			player.MakeBet();
		
		// Give the players and yourself 2 cards
		Dealer.BeginGame(Players, _deck);
		
		// If dealer blackjack
		if (Dealer.HasBlackjack)
		{
			OnEnd?.Invoke();
			Finish();
			KickBankrupts();
			return;
		}

		// Players taking cards
		foreach (Player player in Players)
		{
			while (player.Action == PlayerAction.Hit)
			{
				player.TakeCard(_deck.GetCard());
				player.OnHit?.Invoke();
			}
			player.OnStand?.Invoke();
		}
		
		// Take cards while the score is less than 17
		Dealer.GetCards(_deck);
		
		OnEnd?.Invoke();
		Finish();
		KickBankrupts();
	}

	private void Finish()
	{
		Dealer.Finish();
		foreach (Player player in Players)
			player.Finish(Dealer);
	}

	private void KickBankrupts()
	{
		Players = Players.Where(player =>
		{
			if (player.Money == 0)
				player.OnKick?.Invoke();
			return player.Money > 0;
		}).ToList();
	}
}