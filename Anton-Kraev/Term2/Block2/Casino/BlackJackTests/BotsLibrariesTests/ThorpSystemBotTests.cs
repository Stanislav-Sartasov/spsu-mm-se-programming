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
            bot.Hands[0].Cards.Add(new Card(CardRank.Nine));
            bot.Hands[0].Cards.Add(new Card(CardRank.Ten));
            var shoes = new Shoes();
            shoes.Decks[CardRank.Two] = 22;
            bot.Play(new Card(CardRank.Seven), shoes);
            Assert.AreEqual(26, bot.CurrentBet);
        }
    }
}