using NUnit.Framework;
using DeckLibrary;
using Blackjack.PlayerLibrary;
using System.Collections.Generic;

namespace Blackjack.Players.UnitTests
{
	public class Hit17PlayerTests
	{
		private static readonly Card Two = new(CardSuits.Club, CardRanks.Two);
		private static readonly Card Ten = new(CardSuits.Club, CardRanks.Ten);
		private static readonly Card Ace = new(CardSuits.Club, CardRanks.Ace);

		[Test]
		public void BettingTest()
		{
			Hit17Player player = new();
			int initial = 250;
			int minBet = 50;
			player.AddChips(initial);
			player.PlaceBet(minBet);
			Assert.True(player.GetCurrentStack() == initial - minBet);
		}

		[Test]
		public void PlayerHitTest()
		{
			Hit17Player player = new();
			Assert.AreEqual(Moveset.Hit, player.MakeMove(Ace, new List<Card>() { Ten, Two, Two }));
		}

		[Test]
		public void PlayerStandTest()
		{
			Hit17Player player = new();
			Assert.AreEqual(Moveset.Stand, player.MakeMove(Ace, new List<Card>() { Ten, Ace }));
		}
	}
}
