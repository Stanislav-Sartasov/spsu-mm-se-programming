using Blackjack.DealerLibrary;
using DeckLibrary;

namespace Blackjack.TestingDummies
{
	public class DealerDeckEater : AbstractDealer
	{
		public override bool ToGetNextCard(List<Card> dealerHand, List<Card> playerHand)
		{
			return dealerHand.Count < 8 * 52 + 100;
		}

		public override Decisionset MakeDecision(List<Card> dealerHand, List<Card> playerHand)
		{
			return Decisionset.DealerWins;
		}
	}
}
