using BaseBot;
using BotStructure;
using Cards;
using NUnit.Framework;

namespace BotsLibrariesTests
{
    public class BaseBotTests
    {
        private Bot bot;

        [SetUp]
        public void Setup()
        {
            bot = new BaseStrategyBot(100);
        }

        [Test]
        public void SplitTest()
        {
            bot.Hands[0].Cards.Add(new Card(CardRank.Nine));
            bot.Hands[0].Cards.Add(new Card(CardRank.Nine));
            bot.Move(0, new Card(CardRank.Three), new Shoes());
            Assert.AreEqual(2, bot.Hands.Count);
            Assert.AreEqual(CardRank.Nine, bot.Hands[0].Cards[0].Rank);
            Assert.AreEqual(CardRank.Nine, bot.Hands[1].Cards[0].Rank);
        }

        [Test]
        public void DoubleDownTest()
        {
            bot.Hands[0].Cards.Add(new Card(CardRank.Five));
            bot.Hands[0].Cards.Add(new Card(CardRank.Five));
            bot.Move(0, new Card(CardRank.Five), new Shoes());
            Assert.AreEqual(1, bot.Hands.Count);
            Assert.AreEqual(3, bot.Hands[0].Cards.Count);
            Assert.AreEqual(44, bot.Hands[0].Bet);
        }

        [Test]
        public void TakingCardTest()
        {
            bot.Hands[0].Cards.Add(new Card(CardRank.Eight));
            bot.Hands[0].Cards.Add(new Card(CardRank.Eight));
            bot.Move(0, new Card(CardRank.Ace), new Shoes());
            Assert.AreEqual(1, bot.Hands.Count);
            Assert.AreEqual(3, bot.Hands[0].Cards.Count);
            Assert.AreEqual(22, bot.Hands[0].Bet);
        }
    }
}