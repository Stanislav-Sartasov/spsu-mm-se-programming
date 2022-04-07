using NUnit.Framework;
using DeckLibrary;
using System.Collections.Generic;
using Blackjack.PlayerLibrary;

namespace Blackjack.Players.UnitTests
{
	internal class BasicStrategyPlayerTests
	{
		private static readonly Card Two = new(CardSuits.Club, CardRanks.Two);
		private static readonly Card Ten = new(CardSuits.Club, CardRanks.Ten);
		private static readonly Card Ace = new(CardSuits.Club, CardRanks.Ace);

		[Test]
		public void BettingTest()
		{
			BasicStrategyPlayer player = new();
			int initial = 100;
			int minBet = 20;
			player.AddChips(initial);
			Assert.AreEqual(minBet, player.PlaceBet(minBet));
		}

		[Test]
		public void HitOnSmallValueTest()
		{
			BasicStrategyPlayer player = new();
			List<Card> playerHand = new() { Ten };
			Assert.AreEqual(Moveset.Hit, player.MakeMove(Ace, playerHand));
		}

		[Test]
		public void StandOnBigValueTest()
		{
			BasicStrategyPlayer player = new();
			List<Card> playerHand = new() { Ten, Ten };
			Assert.AreEqual(Moveset.Stand, player.MakeMove(Ace, playerHand));
		}

		[Test]
		public void HitOnBigDealerValueTest()
		{
			BasicStrategyPlayer player = new();
			List<Card> playerHand = new() { Ten, Two, Two, Two };
			Assert.AreEqual(Moveset.Hit, player.MakeMove(Ace, playerHand));
		}

		[Test]
		public void StandOnSmallDealerValueTest()
		{
			BasicStrategyPlayer player = new();
			List<Card> playerHand = new() { Ten, Two, Two, Two };
			Assert.AreEqual(Moveset.Stand, player.MakeMove(Two, playerHand));
		}
	}
}
