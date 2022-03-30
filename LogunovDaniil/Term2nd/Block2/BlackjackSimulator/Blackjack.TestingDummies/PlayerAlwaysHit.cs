using DeckLibrary;
using Blackjack.PlayerLibrary;
using Blackjack.GeneralInfo;

namespace Blackjack.TestingDummies
{
	public class PlayerAlwaysHit : AbstractPlayer
	{
		public override Moveset MakeMove(Card dealerHand, List<Card> playerHand)
		{
			return Moveset.Hit;
		}
	}
}
