using Blackjack.PlayerLibrary;
using DeckLibrary;
using Blackjack.GeneralInfo;

namespace Blackjack.Players
{
	public class Hit17Player : AbstractPlayer
	{
		protected override int GetNextBet(int minBet)
		{
			return minBet;
		}

		public override Moveset MakeMove(Card dealerHand, List<Card> playerHand)
		{
			return Ruleset.EvaluateHandValue(playerHand) < 17 ? Moveset.Hit : Moveset.Stand;
		}
	}
}
