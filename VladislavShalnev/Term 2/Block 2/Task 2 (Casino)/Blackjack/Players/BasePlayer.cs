using Blackjack.Cards;

namespace Blackjack.Players;

public abstract class BasePlayer
{
	public readonly List<Card> Cards = new List<Card>();

	public int Score { get; private set; }

	public bool HasBlackjack =>
		Score == 21 && Cards.Count == 2;

	// BasePlayer events
	public Action? OnBlackjack;

	public void TakeCard(Card card)
	{
		Cards.Add(card);
		
		if (card.Type == CardType.Ace)
			Score += Score + 11 > 21 ? 1 : 11;
		else
			Score += card.Value;
	}

	public virtual void Clear()
	{
		Cards.Clear();
		Score = 0;
	}
}