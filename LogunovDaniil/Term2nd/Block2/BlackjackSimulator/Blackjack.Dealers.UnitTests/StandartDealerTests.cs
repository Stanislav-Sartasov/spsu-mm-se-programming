using NUnit.Framework;
using DeckLibrary;
using System.Collections.Generic;
using Blackjack.DealerLibrary;

namespace Blackjack.Dealers.UnitTests
{
	public class StandartDealerTests
	{
		private static readonly Card Two = new(CardSuits.Club, CardRanks.Two);
		private static readonly Card Ten = new(CardSuits.Club, CardRanks.Ten);
		private static readonly Card Ace = new(CardSuits.Club, CardRanks.Ace);

		[Test]
		public void MinBetValueRetainingTest()
		{
			StandartDealer dealer = new();
			int first = dealer.GetMinBet();
			Assert.True(first == dealer.GetMinBet());
		}

		[Test]
		public void PlayerHandTooBigTest()
		{
			List<Card> playerHand = new() { Two, Two, Two, Two, Two, Two };
			StandartDealer dealer = new();
			Assert.False(dealer.CanPlayerHit(playerHand));
		}

		[Test]
		public void PlayerHandValueTooBigTest()
		{
			List<Card> playerHand = new() { Ten, Ten, Two };
			StandartDealer dealer = new();
			Assert.False(dealer.CanPlayerHit(playerHand));
		}

		[Test]
		public void DealerHandValueTooMuchTest()
		{
			List<Card> dealerHand = new() { Ten, Ten };
			StandartDealer dealer = new();
			Assert.False(dealer.ToGetNextCard(dealerHand, new List<Card>()));
		}

		[Test]
		public void TieDecisionTest()
		{
			List<Card> dealerHand = new() { Ten, Ace };
			List<Card> playerHand = new() { Ace, Ten };
			StandartDealer dealer = new();
			Assert.AreEqual(Decisionset.Tie, dealer.MakeDecision(dealerHand, playerHand));
		}

		[Test]
		public void PlayerWinDecisionTest()
		{
			List<Card> dealerHand = new() { Ten, Ten };
			List<Card> playerHand = new() { Ace, Ten };
			StandartDealer dealer = new();
			Assert.AreEqual(Decisionset.PlayerWins, dealer.MakeDecision(dealerHand, playerHand));
		}

		[Test]
		public void DealerWinDecisionTest()
		{
			List<Card> dealerHand = new() { Ten, Ace };
			List<Card> playerHand = new() { Ten, Ten };
			StandartDealer dealer = new();
			Assert.AreEqual(Decisionset.DealerWins, dealer.MakeDecision(dealerHand, playerHand));
		}

		[Test]
		public void FiveCardPlayerWinDecisionTest()
		{
			List<Card> dealerHand = new() { Ten, Ace };
			List<Card> playerHand = new() { Two, Two, Two, Two, Two };
			StandartDealer dealer = new();
			Assert.AreEqual(Decisionset.PlayerWins, dealer.MakeDecision(dealerHand, playerHand));
		}

		[Test]
		public void BustPlayerLostDecisionTest()
		{
			List<Card> dealerHand = new() { Ten, Two };
			List<Card> playerHand = new() { Ten, Ten, Ten };
			StandartDealer dealer = new();
			Assert.AreEqual(Decisionset.DealerWins, dealer.MakeDecision(dealerHand, playerHand));
		}

		[Test]
		public void BustDealerLostDecisionTest()
		{
			List<Card> playerHand = new() { Ten, Two };
			List<Card> dealerHand = new() { Ten, Ten, Ten };
			StandartDealer dealer = new();
			Assert.AreEqual(Decisionset.PlayerWins, dealer.MakeDecision(dealerHand, playerHand));
		}

		[Test]
		public void PlayerBlackjackWinningTest()
		{
			List<Card> dealerHand = new() { Ten, Ten };
			List<Card> playerHand = new() { Ace, Ten };
			StandartDealer dealer = new();
			int bet = 50;
			Assert.AreEqual(75, dealer.CalculateWinning(dealerHand, playerHand, bet));
		}

		[Test]
		public void PlayerNormalWinningTest()
		{
			List<Card> dealerHand = new() { Ten, Two, Two, Two };
			List<Card> playerHand = new() { Ten, Ten };
			StandartDealer dealer = new();
			int bet = 50;
			Assert.AreEqual(50, dealer.CalculateWinning(dealerHand, playerHand, bet));
		}
	}
}