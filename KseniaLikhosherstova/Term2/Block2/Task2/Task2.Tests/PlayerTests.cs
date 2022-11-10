using Game.Cards;
using Game.Players;


namespace Task2.Tests
{

    public class PlayerTests
    {
        [Test]
        public void ClearHandTest()
        {
            Player player = new Player("Bob", 2000);
            Card card = new Card(Rank.Eight, Suit.Spades);
            player.Hit(card);
            player.ClearHand();
            Assert.That(player.Cards.Count, Is.EqualTo(0));

            Assert.Pass();
        }


        [Test]

        public void HitTest()
        {
            PlayerState state = PlayerState.Playing;
            Player player = new Player("Bob", 2000);
            Card card = new Card(Rank.Eight, Suit.Spades);
            for (int i = 0; i < 10; i++)
                player.Hit(card);
            Assert.That(player.Cards.Count, Is.EqualTo(10));
            Assert.IsTrue(player.State == state);

            Assert.Pass();
        }

        [Test]

        public void TakeMoneyTest()
        {
            Player player = new Player("Helga", 325);
            player.TakeMoney(43);
            Assert.That(player.Money, Is.EqualTo(368));

            Assert.Pass();
        }

    }

}

