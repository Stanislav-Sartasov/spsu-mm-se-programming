using DeckLibrary;
using Blackjack.GeneralInfo;

namespace Blackjack.PlayerLibrary
{
	public abstract class AbstractPlayer
	{
		// all stack changes were blocked in inherited classes
		// so that every possible Player's stack has the same functionality
		// and the value itself is not being manipulated
		// although, they can still access and see it
		protected int stack { private set; get; } = 0;

		// to signify the game has ended
		// in more advanced strategies, may need to clear their internal data
		public virtual void Flush()
		{
			stack = 0;
		}

		protected virtual int GetNextBet(int minBet)
		{
			return stack;
		}

		public int GetCurrentStack()
		{
			return stack;
		}

		public int PlaceBet(int minBet)
		{
			if (minBet > stack)
				return 0;
			int bet = Math.Max(minBet, GetNextBet(minBet));
			stack -= bet;
			return bet;
		}

		public virtual Moveset MakeMove(Card dealerHand, List<Card> playerHand)
		{
			return Moveset.Stand;
		}

		// yeah about not being manipulated...
		public void AddChips(int amount)
		{
			stack += amount;
		}

		// for more advanced strategies that use information of previous rounds
		// to calculate their next move
		public virtual void RecieveResult(RoundMemo round)
		{
			return;
		}
	}
}