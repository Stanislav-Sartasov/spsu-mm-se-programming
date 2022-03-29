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
            bot.Hands[0].Cards.Add(new Card("9"));
            bot.Hands[0].Cards.Add(new Card("9"));
            bot.Move(0, new Card("3"), new Shoes());
            Assert.AreEqual(2, bot.Hands.Count);
            Assert.AreEqual("9", bot.Hands[0].Cards[0].Name);
            Assert.AreEqual("9", bot.Hands[1].Cards[0].Name);
        }

        [Test]
        public void DoubleDownTest()
        {
            bot.Hands[0].Cards.Add(new Card("4"));
            bot.Hands[0].Cards.Add(new Card("6"));
            bot.Move(0, new Card("6"), new Shoes());
            Assert.AreEqual(1, bot.Hands.Count);
            Assert.AreEqual(3, bot.Hands[0].Cards.Count);
            Assert.AreEqual(44, bot.Hands[0].Bet);
        }

        [Test]
        public void TakingCardTest()
        {
            bot.Hands[0].Cards.Add(new Card("8"));
            bot.Hands[0].Cards.Add(new Card("8"));
            bot.Move(0, new Card("A"), new Shoes());
            Assert.AreEqual(1, bot.Hands.Count);
            Assert.AreEqual(3, bot.Hands[0].Cards.Count);
            Assert.AreEqual(22, bot.Hands[0].Bet);
        }
    }
}