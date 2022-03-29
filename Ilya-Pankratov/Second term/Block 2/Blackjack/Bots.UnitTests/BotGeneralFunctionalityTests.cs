using NUnit.Framework;
using GameTools;

namespace Bots.UnitTests
{
    public class BotGeneralFunctionalityTests
    {
        [Test]
        public void ConstructorTest()
        {
            // first case

            var firstBot = new WinningStrategyBot();
            Assert.IsFalse(firstBot.IsLeave());
            Assert.IsTrue(firstBot.GamePlayed == 0);
            Assert.IsTrue(firstBot.Hands != null);
            Assert.IsTrue(firstBot.Cash == 1000);
            Assert.IsTrue(firstBot.Flag == Player.PlayerState.Play);


            // second case

            var secondBot = new HiOptIIStrategyBot();
            Assert.IsTrue(secondBot.GamePlayed == 0);
            Assert.IsFalse(secondBot.IsLeave());
            Assert.IsTrue(secondBot.Hands != null);
            Assert.IsTrue(secondBot.Cash == 1000);
            Assert.IsTrue(secondBot.Flag == Player.PlayerState.Play);
        }

        [Test]
        public void StopFlagTest()
        {
            var bot = new WinningStrategyBot();
            var  dealerCards = Tools.CreateDealerCardSet(CardRank.Five);
            bot.Hands[0].Cards = Tools.CreateCardSet(CardRank.Ace);
            bot.Flag = Player.PlayerState.Stop;
            Assert.IsTrue(bot.Play(bot.Hands[0], dealerCards) == HandState.Stand);
        }

        [Test]
        public void PlaySameCardsTest()
        {
            // preparation 

            var dealerCards = Tools.CreateDealerCardSet(CardRank.Five);
            var bot = new WinningStrategyBot(i => false);
            bot.MakeBet(5);
            var botHand = bot.Hands[0];

            // A/A split checking

            botHand.Cards = Tools.CreateCardSet(CardRank.Ace);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Split);

            // A/A hit checking

            dealerCards = Tools.CreateDealerCardSet(CardRank.Ace);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // 10/10 stand checking

            botHand.Cards = Tools.CreateCardSet(CardRank.Ten);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Stand);

            // 5/5 double checking

            botHand.Cards = Tools.CreateCardSet(CardRank.Five);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Double);

            // 4/4 split checking

            dealerCards = Tools.CreateDealerCardSet(CardRank.Six);
            botHand.Cards = Tools.CreateCardSet(CardRank.Four);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Split);

            // 4/4 hit checking

            dealerCards = Tools.CreateDealerCardSet(CardRank.Seven);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // 9/9 stand checking

            botHand.Cards = Tools.CreateCardSet(CardRank.Nine);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Stand);

            // 9/9 split checking

            dealerCards = Tools.CreateDealerCardSet(CardRank.Eight);
            botHand.Cards = Tools.CreateCardSet(CardRank.Nine);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Split);

            // 6/6 hit checking

            dealerCards = Tools.CreateDealerCardSet(CardRank.Seven);
            botHand.Cards = Tools.CreateCardSet(CardRank.Six);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // 2/2 3/3 6/6 7/7 hit checking

            dealerCards = Tools.CreateDealerCardSet(CardRank.Eight);
            botHand.Cards = Tools.CreateCardSet(CardRank.Six);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // 2/2 3/3 6/6 7/7 split checking

            dealerCards = Tools.CreateDealerCardSet(CardRank.Two);
            botHand.Cards = Tools.CreateCardSet(CardRank.Six);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Split);
        }
        
        [Test]
        public void PlayHardTotalsTest()
        {
            // preparetion 

            var dealerCards = Tools.CreateDealerCardSet(CardRank.Five);
            var bot = new WinningStrategyBot();
            bot.MakeBet(5);
            var botHand = bot.Hands[0];

            // checking 4-8 hit

            botHand.Cards = Tools.CreateCardSet(CardRank.Two, CardRank.Three);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // checking 9 hit

            botHand.Cards = Tools.CreateCardSet(CardRank.Two, CardRank.Seven);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // checking 9 double

            dealerCards = Tools.CreateDealerCardSet(CardRank.Ace);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Double);

            // checking 9 double

            dealerCards = Tools.CreateDealerCardSet(CardRank.Ace);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Double);

            // checking hit when player has 11 points & dealer has 11  points 

            botHand.Cards = Tools.CreateCardSet(CardRank.Five, CardRank.Six);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // checking hit when player has 10 points & dealer has 10-11  points 

            botHand.Cards = Tools.CreateCardSet(CardRank.Six, CardRank.Four);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // checking 10-11 double

            dealerCards = Tools.CreateDealerCardSet(CardRank.Five);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Double);

            // checking 12 stand

            botHand.Cards = Tools.CreateCardSet(CardRank.Eight, CardRank.Four);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Stand);

            // checking 12 hit

            dealerCards = Tools.CreateDealerCardSet(CardRank.Ace);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // checking 15-16 surrender

            botHand.Cards = Tools.CreateCardSet(CardRank.Eight, CardRank.Seven);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Surrender);

            // checking 13-16 Hit

            dealerCards = Tools.CreateDealerCardSet(CardRank.Nine);
            botHand.Cards = Tools.CreateCardSet(CardRank.Eight, CardRank.Seven);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // checking 13-16 Stand

            dealerCards = Tools.CreateDealerCardSet(CardRank.Two);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Stand);

            // checking 17-21 stand

            botHand.Cards = Tools.CreateCardSet(CardRank.Nine, CardRank.Ten);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Stand);

            // play hard totals, when have not 2 cards in hand

            botHand.Cards.Clear();
            botHand.Cards.Add(new Card(CardSuit.Diamond, CardRank.Nine));
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);
        }

        [Test]
        public void PlaySoftTotalsTest()
        {
            // preparetion 

            var dealerCards = Tools.CreateDealerCardSet(CardRank.Five);
            var bot = new WinningStrategyBot();
            bot.MakeBet(5);
            var botHand = bot.Hands[0];

            // checking 13-17 double

            botHand.Cards = Tools.CreateCardSet(CardRank.Ace, CardRank.Three);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Double);

            // checking 13-17 hit

            dealerCards = Tools.CreateDealerCardSet(CardRank.Nine);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // checking 18 Hit

            botHand.Cards = Tools.CreateCardSet(CardRank.Ace, CardRank.Seven);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Hit);

            // checking 18 stand

            dealerCards = Tools.CreateDealerCardSet(CardRank.Eight);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Stand);

            // checking 19-21 stand

            botHand.Cards = Tools.CreateCardSet(CardRank.Ace, CardRank.Ten);
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Stand);

            // one ace -> double | checking one ace in hand after split

            botHand.Cards.Clear();
            botHand.Cards.Add(new Card(CardSuit.Diamond, CardRank.Ace));
            Assert.IsTrue(bot.Play(botHand, dealerCards) == HandState.Double);
        }
    }
}