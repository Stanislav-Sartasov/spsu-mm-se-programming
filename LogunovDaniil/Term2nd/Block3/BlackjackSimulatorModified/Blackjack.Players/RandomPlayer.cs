using Blackjack.PlayerLibrary;
using DeckLibrary;

namespace Blackjack.Players
{
	public class RandomPlayer : AbstractPlayer
	{
		private readonly Random rnd = new();

		protected override int GetNextBet(int minBet)
		{
			return (int)(rnd.NextDouble() * stack / 4);
		}

		public override Moveset MakeMove(Card dealerHand, List<Card> playerHand)
		{
			return rnd.NextDouble() > 0.5 ? Moveset.Stand : Moveset.Hit;
		}
	}
}