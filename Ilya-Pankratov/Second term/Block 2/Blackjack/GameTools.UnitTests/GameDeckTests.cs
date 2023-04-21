using NUnit.Framework;

namespace GameTools.UnitTests
{
    public class GameDeckTests
    {
        [Test]
        public void ConstructorTest()
        {
            // preparation 

            int amountOfDecks = 8;
            var deck = new GameDeck(amountOfDecks);

            // checking

            Assert.IsNotNull(deck);
            Assert.IsNotNull(deck.Cards);
            Assert.IsTrue(deck.Cards.Count == amountOfDecks * 52);
        }
    }
}
