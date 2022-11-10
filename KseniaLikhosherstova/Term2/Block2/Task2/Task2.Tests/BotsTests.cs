using Game.Cards;
using Bots;
using Game.Players;


namespace Task2.Tests
{
    public class BotsTests
    {
        [Test]

        public void StupidBotTest()
        {
            StupidBot stupidBot = new StupidBot("Terry Benedict", 1345);
            Card cardOne = new Card(Rank.Two, Suit.Spades);
            Card cardTwo = new Card(Rank.Nine, Suit.Clubs);

            Assert.That(stupidBot.Bet(), Is.EqualTo(269));

            stupidBot.Hit(cardOne);
            stupidBot.Hit(cardTwo);

            Assert.That(stupidBot.Move(), Is.EqualTo(PlayerAction.Hit));

            Assert.Pass();
        }

        [Test]

        public void StandardBotTest()
        {

            StandardBot standardBot = new StandardBot("Rusty Ryan", 897);
            Card cardOne = new Card(Rank.King, Suit.Hearts);
            Card cardTwo = new Card(Rank.Ace, Suit.Diamonds);

            Assert.That(standardBot.Bet(), Is.EqualTo(50));

            standardBot.Hit(cardOne);
            standardBot.Hit(cardTwo);

            Assert.That(standardBot.Move(), Is.EqualTo(PlayerAction.Stand));

            Assert.Pass();
        }

        public void SmartBotTest()
        {
            SmartBot smartBot = new SmartBot("Danny Ocean", 1055);
            Card cardOne = new Card(Rank.Two, Suit.Hearts);
            Card cardTwo = new Card(Rank.Ace, Suit.Diamonds);

            Assert.That(smartBot.Bet(), Is.EqualTo(147));

            smartBot.Hit(cardOne);
            smartBot.Hit(cardTwo);

            Assert.That(smartBot.Move(), Is.EqualTo(PlayerAction.Double));

            Assert.That(smartBot.Cards.Count, Is.EqualTo(3));

            Assert.Pass();
        }

    }
}

