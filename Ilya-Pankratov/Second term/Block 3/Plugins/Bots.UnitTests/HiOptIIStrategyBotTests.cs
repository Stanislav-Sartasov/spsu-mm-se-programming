using NUnit.Framework;
using System.Collections.Generic;
using Player;
using GameTools;

namespace Bots.UnitTests
{
    public class HiOptIIStrategyBotTests
    {

        [Test]
        public void MakeBetTest()
        {
            // preparetion

            int initialBet = 100;
            var bot = new HiOptIIStrategyBot(initialBet)
            {
                Flag = PlayerState.Stop
            };
            var botHand = bot.Hands[0];
            botHand.Cards = Tools.CreateCardSet(CardRank.Ten);

            List<Card> dealerCards = Tools.CreateCardSet(CardRank.Ace, CardRank.Two);

            int minBet = 10;
            int betUnit = 2 * minBet;

            // checking bet when realScore < 0

            bot.Play(botHand, dealerCards);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit / 2);

            // checking bet when 0 <= realScore < 1

            botHand.Cards = Tools.CreateCardSet(CardRank.Four);
            dealerCards = Tools.CreateCardSet(CardRank.Five);

            Tools.RepeatPlay(bot, dealerCards, 3);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit);

            // checking bet when 1 <= realScore < 2 

            Tools.RepeatPlay(bot, dealerCards, 1);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit * 2);

            // checking bet when 2 <= realScore < 3

            Tools.RepeatPlay(bot, dealerCards, 1);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit * 4);

            // checking bet when 3 <= realScore < 4 

            Tools.RepeatPlay(bot, dealerCards, 1);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit * 6);

            // checking bet when 4 <= realScore < 5 

            Tools.RepeatPlay(bot, dealerCards, 1);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit * 8);

            // checking bet when 5 <= realScore < 6 

            Tools.RepeatPlay(bot, dealerCards, 1);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit * 10);

            // checking bet when realScore >= 6 

            Tools.RepeatPlay(bot, dealerCards, 1);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit * 12);
        }
    }
}