using Blackjack.Cards;
using NUnit.Framework;

namespace Blackjack.Players.UnitTests;

public class BasePlayerTests
{
	[Test]
	public void TakeCardTest()
	{
		BasePlayer basePlayer = new Dealer();
		basePlayer.TakeCard(new Card(CardSuit.Diamonds, CardType.Two));
		
		Assert.AreEqual(1, basePlayer.Cards.Count);
		Assert.AreEqual(2, basePlayer.Score);
	}
	
	[Test]
	public void ClearTest()
	{
		BasePlayer basePlayer = new Dealer();
		basePlayer.TakeCard(new Card(CardSuit.Diamonds, CardType.Two));
		
		Assert.AreEqual(1, basePlayer.Cards.Count);
		Assert.AreEqual(2, basePlayer.Score);
		
		basePlayer.Clear();
		
		Assert.AreEqual(0, basePlayer.Cards.Count);
		Assert.AreEqual(0, basePlayer.Score);
	}
}