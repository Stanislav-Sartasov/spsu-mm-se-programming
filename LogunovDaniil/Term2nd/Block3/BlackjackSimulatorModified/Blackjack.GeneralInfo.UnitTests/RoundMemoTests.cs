using NUnit.Framework;
using System.Collections.Generic;
using DeckLibrary;

namespace Blackjack.GeneralInfo.UnitTests
{
	public class RoundMemoTests
	{
		private static readonly Card Ace = new(CardSuits.Club, CardRanks.Ace);
		private static readonly Card Six = new(CardSuits.Diamond, CardRanks.Six);
		private static readonly Card Ten = new(CardSuits.Heart, CardRanks.Ten);

		private static readonly List<Card> Dealer = new List<Card> { Ace, Six, Six };
		private static readonly List<Card> Player = new List<Card> { Ten, Ace };

		private static readonly int Bet = 15;
		private static readonly int Winnings = 30;

		private static readonly RoundMemo TestRound = new(Dealer, Player, Bet, Winnings, RoundResult.PlayerWon);
		private static readonly RoundMemo SkippedRound = new(RoundResult.InvalidBet);

		[Test]
		public void ValueRetainingTest()
		{
			Assert.AreEqual(Bet, TestRound.PlayerBet);
			Assert.AreEqual(Winnings, TestRound.PlayerReturn);
			Assert.AreEqual(RoundResult.PlayerWon, TestRound.Result);
		}

		[Test]
		public void GetDealersHandCopyTest()
		{
			Assert.AreEqual(Dealer, TestRound.GetDealersHandCopy());
		}

		[Test]
		public void GetPlayersHandCopyTest()
		{
			Assert.AreEqual(Player, TestRound.GetPlayersHandCopy());
		}

		[Test]
		public void NoCardsInSkippedRoundTest()
		{
			Assert.AreEqual(new List<Card>(), SkippedRound.GetDealersHandCopy());
		}
	}
}
