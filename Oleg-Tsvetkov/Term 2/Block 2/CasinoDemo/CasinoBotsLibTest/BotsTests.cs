using NUnit.Framework;
using CasinoBotsLib;
using CasinoLib;
using System;

namespace CasinoBotsLibTest
{
    public class BotsTests
    {
        Roulette roulette;
        [SetUp]
        public void Setup()
        {
            roulette = new Roulette();
        }

        [Test]
        public void TestMartingaleBot()
        {
            MartingaleBotPlayer martingaleBot = new MartingaleBotPlayer(1000);
            for (int i = 0; i < 40 && martingaleBot.Balance > 0; ++i)
            {
                Int64 balanceBeforeBet = martingaleBot.Balance;
                martingaleBot.PlaceBetAndPlay(roulette);
                if (martingaleBot.IsLastWon)
                {
                    Assert.IsTrue(balanceBeforeBet + martingaleBot.LastBetValue == martingaleBot.Balance);
                }
                else
                {
                    Assert.IsTrue(balanceBeforeBet - martingaleBot.LastBetValue == martingaleBot.Balance);
                }
            }
        }

        [Test]
        public void TestYoloBot()
        {
            YoloBotPlayer yoloBot = new YoloBotPlayer(1000);
            for (int i = 0; i < 40 && yoloBot.Balance > 0; ++i)
            {
                Int64 balanceBeforeBet = yoloBot.Balance;
                yoloBot.PlaceBetAndPlay(roulette);
                if (yoloBot.IsLastWon)
                {
                    Assert.IsTrue(balanceBeforeBet + 35*yoloBot.BaseBetValue == yoloBot.Balance);
                }
                else
                {
                    Assert.IsTrue(balanceBeforeBet - yoloBot.BaseBetValue == yoloBot.Balance);
                }
            }
        }

        [Test]
        public void TestRandomParityBot()
        {
            RandomParityBotPlayer parityBot = new RandomParityBotPlayer(1000);
            for (int i = 0; i < 40 && parityBot.Balance > 0; ++i)
            {
                Int64 balanceBeforeBet = parityBot.Balance;
                parityBot.PlaceBetAndPlay(roulette);
                if (parityBot.IsLastWon)
                {
                    Assert.IsTrue(balanceBeforeBet + parityBot.BaseBetValue == parityBot.Balance);
                }
                else
                {
                    Assert.IsTrue(balanceBeforeBet - parityBot.BaseBetValue == parityBot.Balance);
                }
            }
        }

        [Test]
        public void TestPlayAverageBets()
        {
            RandomParityBotPlayer parityBot = new RandomParityBotPlayer(1000);
            parityBot.PlayAverageBetsForBot(roulette, 40);
            Assert.IsTrue(parityBot.Balance >= 0);
        }
    }
}