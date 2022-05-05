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
            bot.Hands[0].Cards.Add(new Card(CardRank.Nine));
            bot.Hands[0].Cards.Add(new Card(CardRank.Queen));
            var shoes = new Shoes();
            shoes.Decks[CardRank.Three] = 0;
            bot.Play(new Card(CardRank.Seven), shoes);
            Assert.AreEqual(40, bot.CurrentBet);
        }
    }
}