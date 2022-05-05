using DeckLibrary;

namespace Blackjack.DealerLibrary
{
	public class AbstractDealer
	{
		public virtual int GetMinBet()
		{
			return 0;
		}

		public virtual bool CheckCanPlayerHit(List<Card> playerHand)
		{
			return false;
		}

		public virtual int CalculateWinning(List<Card> dealerHand, List<Card> playerHand, int bet)
		{
			return 0;
		}

		public virtual bool ToGetNextCard(List<Card> dealerHand, List<Card> playerHand)
		{
			return false;
		}

		public virtual Decisionset MakeDecision(List<Card> dealerHand, List<Card> playerHand)
		{
			return Decisionset.Tie;
		}
	}
}