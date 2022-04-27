using Blackjack.PlayerLibrary;
using Blackjack.DealerLibrary;
using Blackjack.GeneralInfo;
using DeckLibrary;

namespace Blackjack.GameTable
{
	public class GameTable
	{
		private static readonly int DecksInUse = 8;
		private static readonly double DeckReshuffleThreshold = 0.5;
		private static readonly Card BlankCard = new(CardSuits.Club, CardRanks.Ten);

		private readonly AbstractPlayer player;
		private readonly AbstractDealer dealer;
		private Deck deck;

		private bool wasDeckEmptied;

		private void RenewDeck()
		{
			if (deck.GetPercentageLeft() > DeckReshuffleThreshold)
				deck.Reshuffle();
			wasDeckEmptied = false;
		}

		private Card DrawNext()
		{
			Card? card = deck.DrawNextCard();
			if (card == null)
			{
				wasDeckEmptied = true;
				return BlankCard;
			}
			return card;
		}

		public GameTable(AbstractPlayer newPlayer, AbstractDealer newDealer)
		{
			player = newPlayer;
			dealer = newDealer;
			deck = new Deck(DecksInUse);
			wasDeckEmptied = false;
		}

		public RoundMemo RunNewRound()
		{
			RenewDeck();
			int stack = player.GetCurrentStack();
			int minBet = dealer.GetMinBet();
			int bet = player.PlaceBet(minBet);
			List<Card> dealerHand = new();
			List<Card> playerHand = new();
			int toReturn = 0;
			RoundResult result;
			if (bet == 0)
				result = RoundResult.PlayerChickened;
			else if (bet < minBet || bet > stack)
				result = RoundResult.InvalidBet;
			else
			{
				// initial cards set-up
				playerHand.Add(DrawNext());
				dealerHand.Add(DrawNext());
				playerHand.Add(DrawNext());

				while (dealer.CheckCanPlayerHit(playerHand)
					&& player.MakeMove(dealerHand.First(), playerHand) == Moveset.Hit)
					playerHand.Add(DrawNext());

				// opening dealer's second card
				dealerHand.Add(DrawNext());
				while (dealer.ToGetNextCard(dealerHand, playerHand))
					dealerHand.Add(DrawNext());

				switch (dealer.MakeDecision(dealerHand, playerHand))
				{
					case Decisionset.Tie:
						result = RoundResult.Tie;
						toReturn = bet;
						break;
					case Decisionset.PlayerWins:
						int winnings = dealer.CalculateWinning(dealerHand, playerHand, bet);
						toReturn = bet + winnings;
						result = RoundResult.PlayerWon;
						break;
					default: // DealerWins
						result = RoundResult.DealerWon;
						break;
				}
			}

			// invalidating round if the deck got empty during the round
			// this safety code is much shorter than checking every card drawn
			if (wasDeckEmptied)
			{
				result = RoundResult.DeckRunOut;
				toReturn = bet;
			}

			RoundMemo round = new(dealerHand, playerHand, bet, toReturn, result);

			player.AddChips(toReturn);
			player.RecieveResult(round);
			return round;
		}
	}
}
