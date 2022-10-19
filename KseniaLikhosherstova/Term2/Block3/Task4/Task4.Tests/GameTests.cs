using Game.Players;
using Bots;
using Game.Cards;


namespace Task4.Tests
{
    public class GameTests
    {


        [Test]
        public void DealTest()
        {

            Player player1 = new StupidBot("John Doe", 1000);
            Player player2 = new StandardBot("Mike", 1000);
            Player player3 = new SmartBot("Jane", 1000);
            List<Player> players = new List<Player> { player1, player2, player3 };
            Game.BlackJack game = new Game.BlackJack(8, players);
            game.Deal();
            foreach (var player in players)
                Assert.That(player.Cards.Count, Is.EqualTo(2));


            Assert.Pass();
        }


        [Test]
        public void CanDealerHitTest()
        {
            List<Player> players = new List<Player>();
            Dealer dealer = new Dealer();
            Card cardOne = new Card(Rank.Eight, Suit.Spades);
            Card cardTwo = new Card(Rank.Nine, Suit.Clubs);
            Game.BlackJack game = new Game.BlackJack(8, players);
            dealer.Hit(cardOne);
            dealer.Hit(cardTwo);
            Assert.IsFalse(game.CanDealerHit(dealer));

            Assert.Pass();
        }


        [Test]
        public void IsBustedTest()
        {
            Player player = new Player("David", 10234);
            List<Player> players = new List<Player>() { player };
            Game.BlackJack game = new Game.BlackJack(8, players);
            for (int i = 11; i < 14; i++)
                for (int j = 1; j < 3; j++)
                    player.Hit(new Card((Rank)i, (Suit)j));
            Assert.IsTrue(game.IsBusted(player));

            Assert.Pass();
        }


        [Test]
        public void HasBlackJackTest()
        {
            List<Player> players = new List<Player>();
            Dealer dealer = new Dealer();
            Card cardOne = new Card(Rank.Ten, Suit.Spades);
            Card cardTwo = new Card(Rank.Ace, Suit.Clubs);
            Game.BlackJack game = new Game.BlackJack(8, players);
            dealer.Hit(cardOne);
            dealer.Hit(cardTwo);
            Assert.IsTrue(game.HasBlackJack(dealer));

            Assert.Pass();
        }
    }
}
