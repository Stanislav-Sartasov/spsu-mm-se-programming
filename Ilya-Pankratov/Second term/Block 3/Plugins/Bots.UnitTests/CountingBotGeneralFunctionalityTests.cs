using NUnit.Framework;
using GameTools;
using Player;

namespace Bots.UnitTests
{
    public class CountingBotGeneralFunctionalityTests
    {
        private readonly int initialBet = 100;

        [Test]
        public void ConstructorTest()
        {
            // first case

            var firstBot = new HiOptIIStrategyBot(x => false, initialBet);
            Assert.IsFalse(firstBot.IsLeave());
            Assert.IsTrue(firstBot.Hands != null);
            Assert.IsTrue(firstBot.Cash == initialBet);
            Assert.IsTrue(firstBot.Flag == PlayerState.Play);

            // second case

            var secondBot = new HiLowStrategyBot(x => false, initialBet);
            Assert.IsFalse(secondBot.IsLeave());
            Assert.IsTrue(secondBot.Hands != null);
            Assert.IsTrue(secondBot.Cash == initialBet);
            Assert.IsTrue(secondBot.Flag == PlayerState.Play);
        }

        [Test]
        public void PlayTest()
        {
            var bot = new HiLowStrategyBot(initialBet);
            bot.Hands[0].Cards = Tools.CreateCardSet(CardRank.Ace);
            var dealerCards = Tools.CreateCardSet(CardRank.Two);

            Assert.IsTrue(bot.Play(bot.Hands[0], dealerCards) == HandState.Split);
        }

        [Test]
        public void ResetFlagTest()
        {
            var bot = new HiLowStrategyBot(initialBet)
            {
                Flag = PlayerState.DeckReset
            };
            bot.Hands[0].Cards = Tools.CreateCardSet(CardRank.Ace);
            var dealerCards = Tools.CreateCardSet(CardRank.Two);

            Assert.IsTrue(bot.Play(bot.Hands[0], dealerCards) == HandState.Done);
        }
    }
}
