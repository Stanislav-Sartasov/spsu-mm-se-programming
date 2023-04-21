using NUnit.Framework;
using Player;
using GameTools;

namespace Bots.UnitTests
{
    public class WinningStrategyBotTests
    {
        [Test]
        public void MakeBetTest()
        {
            // preparetion 

            var bot = new WinningStrategyBot();
            int minBet = 10;

            // checking 

            bot.Cash += minBet;
            bot.MakeBet(minBet);
            int betUnit = 2 * minBet;
            Assert.AreEqual(bot.Hands[0].Bet, betUnit);

            bot.Cash += (int)(1.5 * betUnit);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, 3 * betUnit);

            bot.Cash += (int)(1.5 * 3 * betUnit);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, 2 * betUnit);

            bot.Cash += (int)(1.5 * 2 * betUnit);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, 6 * betUnit);

            bot.Cash += (int)(1.5 * 6 * betUnit);
            bot.MakeBet(minBet);
            Assert.AreEqual(bot.Hands[0].Bet, betUnit);
        }

        [Test]
        public void ResetFlagTest()
        {
            var bot = new WinningStrategyBot(x => false)
            {
                Flag = PlayerState.DeckReset
            };
            bot.Hands[0].Cards = Tools.CreateCardSet(CardRank.Ace);
            var dealerCards = Tools.CreateCardSet(CardRank.Two);

            Assert.IsTrue(bot.Play(bot.Hands[0], dealerCards) == HandState.Done);
        }
    }
}