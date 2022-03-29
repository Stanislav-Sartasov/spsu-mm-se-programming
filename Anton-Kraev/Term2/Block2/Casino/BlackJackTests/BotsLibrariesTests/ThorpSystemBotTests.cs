using BotStructure;
using Cards;
using NUnit.Framework;
using ThorpBot;

namespace BotsLibrariesTests
{
    public class ThorpSystemBotTests
    {
        [Test]
        public void BotPlayTest()
        {
            Bot bot = new ThorpSystemBot(1000);
            bot.Hands[0].Cards.Add(new Card("9"));
            bot.Hands[0].Cards.Add(new Card("10"));
            var shoes = new Shoes();
            shoes.Decks["2"] = 22;
            bot.Play(new Card("7"), shoes);
            Assert.AreEqual(26, bot.CurrentBet);
        }
    }
}