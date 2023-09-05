using DeckLibrary;
using Blackjack.DealerLibrary;

namespace Blackjack.TestingDummies
{
	public class DealerStackDoubler : AbstractDealer
	{
		public override int CalculateWinning(List<Card> dealerHand, List<Card> playerHand, int bet)
		{
			return bet;
		}

		public override Decisionset MakeDecision(List<Card> dealerHand, List<Card> playerHand)
		{
			return Decisionset.PlayerWins;
		}
	}
}
