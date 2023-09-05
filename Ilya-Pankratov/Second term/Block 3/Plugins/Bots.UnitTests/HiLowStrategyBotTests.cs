using NUnit.Framework;
using System.Collections.Generic;
using Player;
using GameTools;

namespace Bots.UnitTests
{
    public class HiLowStrategyBotTests
    {
        

        [Test]
        public void MakeBetTest()
        {
            // preparetion 

            int initialBet = 100;
            var bot = new HiLowStrategyBot(initialBet)
            {
                Flag = PlayerState.Stop
            };
            var botHand = bot.Hands[0];
            botHand.Cards = Tools.CreateCardSet(CardRank.Ace);

            List<Card> dealerCards = Tools.CreateCardSet(CardRank.Ace, CardRank.Eight);

            int minBet = 10;
            int betUnit = 2 * minBet;
           
            // checking bet when 0 <= realScore < 2

            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit);

            // checking bet when realScore < 0

            bot.Play(botHand, dealerCards);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit / 2);

            // checking bet when 2 <= realScore < 3 

            botHand.Cards = Tools.CreateCardSet(CardRank.Two);
            dealerCards = Tools.CreateCardSet(CardRank.Two);

            Tools.RepeatPlay(bot, dealerCards, 8); // play here just increase realScore

            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit * 2);

            // checking bet when 3 <= realScore < 4 

            Tools.RepeatPlay(bot, dealerCards, 2);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit * 4);

            // checking bet when realScore >= 4

            Tools.RepeatPlay(bot, dealerCards, 5);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit * 8);
        }
    }
}