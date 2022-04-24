using System.Collections.Generic;
using BaseBot;
using BotStructure;
using Cards;
using NUnit.Framework;

namespace CasinoTests
{
    public class GameTests
    {
        [Test]
        public void GameTest()
        {
            var shoes = new Shoes();
            shoes.Decks = new Dictionary<CardRank, int>() {{CardRank.Four, 32}};
            shoes.AllCardsCount = 32;
            IBot bot = new BaseStrategyBot(100);
            Casino.Casino.Game(bot, 1, shoes);
            Assert.AreEqual(78, bot.Balance);
            Assert.AreEqual(24, shoes.AllCardsCount);
        }
    }
}