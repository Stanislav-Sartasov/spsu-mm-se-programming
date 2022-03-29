using BotStructure;
using Cards;
using KellyBot;
using NUnit.Framework;

namespace BotsLibrariesTests
{
    public class KellyCriterionBotTests
    {
        [Test]
        public void BotPlayTest()
        {
            Bot bot = new KellyCriterionBot(1000);
            bot.Hands[0].Cards.Add(new Card("9"));
            bot.Hands[0].Cards.Add(new Card("10"));
            var shoes = new Shoes();
            shoes.Decks["3"] = 0;
            bot.Play(new Card("7"), shoes);
            Assert.AreEqual(40, bot.CurrentBet);
        }
    }
}