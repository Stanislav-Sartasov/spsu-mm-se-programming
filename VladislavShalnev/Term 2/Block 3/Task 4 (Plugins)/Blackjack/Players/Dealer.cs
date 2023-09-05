using Blackjack.Cards;

namespace Blackjack.Players;

public class Dealer : BasePlayer
{
	public void BeginGame(List<Player> players, Deck deck)
	{
		foreach (Player player in players)
			for (int i = 0; i < 2; i++)
				player.TakeCard(deck.GetCard());
		
		for (int i = 0; i < 2; i++)
			this.TakeCard(deck.GetCard());
	}

	public void GetCards(Deck deck)
	{
		while (Score < 17)
			this.TakeCard(deck.GetCard());
	}
}