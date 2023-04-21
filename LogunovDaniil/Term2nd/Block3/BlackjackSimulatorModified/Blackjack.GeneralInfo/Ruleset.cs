using DeckLibrary;

namespace Blackjack.GeneralInfo
{
	public static class Ruleset
	{
		private static int EvaluateCardValue(Card card)
		{
			return (char)card.Rank switch
			{
				'T' or 'J' or 'Q' or 'K' => 10,
				'A' => 11,
				_ => (int)card.Rank - (int)'0',
			};
		}

		public static int EvaluateHandValue(List<Card> hand)
		{
			int res = hand.Select(i => EvaluateCardValue(i)).Sum();
			int aces = hand.Count(i => i.Rank == CardRanks.Ace);
			for (int i = 0; i < aces; i++)
				if (res > 21)
					res -= 10;
			return res;
		}

		public static bool IsBlackjack(List<Card> hand)
		{
			if (hand.Count == 2)
				if (hand[0].Rank == CardRanks.Ace && EvaluateCardValue(hand[1]) == 10
					|| hand[1].Rank == CardRanks.Ace && EvaluateCardValue(hand[0]) == 10)
					return true;
			return false;
		}
	}
}
