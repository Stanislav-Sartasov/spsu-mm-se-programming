using DeckLibrary;
using Blackjack.PlayerLibrary;
using Blackjack.GeneralInfo;

namespace Blackjack.Players
{
	public class BasicStrategyPlayer : AbstractPlayer
	{
		protected override int GetNextBet(int minBet)
		{
			return Math.Min(minBet * 3, stack / 5);
		}

		public override Moveset MakeMove(Card dealerHand, List<Card> playerHand)
		{
			int playerValue = Ruleset.EvaluateHandValue(playerHand);
			int dealerValue = Ruleset.EvaluateHandValue(new List<Card>() { dealerHand });
			if (playerValue <= 11)
				return Moveset.Hit;
			if (playerValue >= 17)
				return Moveset.Stand;
			if (dealerValue >= 7)
				return Moveset.Hit;
			if (playerValue >= 13)
				return Moveset.Stand;
			if (dealerValue >= 6)
				return Moveset.Stand;
			return Moveset.Hit;
		}
	}
}
