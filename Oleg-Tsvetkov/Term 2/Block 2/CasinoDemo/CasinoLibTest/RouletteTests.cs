using NUnit.Framework;
using CasinoLib;
using System;

namespace CasinoLibTest
{
    public class RouletteTests
    {
        Roulette roulette;
        private const Int64 testBet = 100;

        [SetUp]
        public void SetupRoulette()
        {
            roulette = new Roulette();
        }

        [Test]
        public void TestRouletteSpin()
        {
            Assert.IsTrue(roulette.LastNumber == -1);
            ColorRouletteBet bet = new ColorRouletteBet(testBet);
            bet.SetColor(BetColorType.Black);
            roulette.Play(bet);
            Assert.IsTrue(roulette.LastNumber >= 0 && roulette.LastNumber <= 36);
        }

        [Test]
        public void TestGetColorCode()
        {
            Assert.IsTrue(roulette.GetColorType(1) == BetColorType.Red);
            Assert.IsTrue(roulette.GetColorType(2) == BetColorType.Black);
            Assert.IsTrue(roulette.GetColorType(0) == BetColorType.Green);
            try
            {
                roulette.GetColorType(999);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Ошибка! Тип ставки за пределом разрешённых значений."));
            }
        }

        [Test]
        public void TestColorBet()
        {
            ColorRouletteBet bet = new(testBet);
            bet.SetColor(BetColorType.Black);
            Int64 result = roulette.Play(bet);

            if (roulette.GetColorType(roulette.LastNumber) == bet.Color)
            {
                Assert.IsTrue(result == testBet);
            }
            else
            {
                Assert.IsTrue(result == -testBet);
            }
        }

        [Test]
        public void TestSingleBet()
        {
            Int64 testNumber = 1;
            SingleRouletteBet bet = new(testBet);
            bet.SetSingle(testNumber);
            Int64 result = roulette.Play(bet);

            if (roulette.LastNumber == testNumber)
            {
                Assert.IsTrue(result == 35*testBet);
            }
            else
            {
                Assert.IsTrue(result == -testBet);
            }
        }

        [Test]
        public void TestDozenBet()
        {
            DozenRouletteBet bet = new(testBet);
            bet.SetDozen(BetDozenType.First);
            Int64 result = roulette.Play(bet);

            if (roulette.LastNumber <= 12 && roulette.LastNumber > 0)
            {
                Assert.IsTrue(result == 2 * testBet);
            }
            else
            {
                Assert.IsTrue(result == -testBet);
            }
        }

        [Test]
        public void TestParityBet()
        {
            ParityRouletteBet bet = new(testBet);
            bet.SetParity(BetParityType.Odd);
            Int64 result = roulette.Play(bet);

            if (roulette.LastNumber % 2 == 1)
            {
                Assert.IsTrue(result == testBet);
            }
            else
            {
                Assert.IsTrue(result == -testBet);
            }
        }

        [Test]
        public void TestBetExceptions()
        {
            Int64 result;
            try
            {
                SingleRouletteBet bet = new(testBet);
                bet.SetSingle(64);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Argument can be between 0 and 36"));
            }
            try
            {
                ColorRouletteBet bet = new(testBet);
                bet.SetColor((BetColorType)64);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Invalid color argument"));
            }
            try
            {
                ParityRouletteBet bet = new(testBet);
                bet.SetParity((BetParityType)64);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Invalid parity argument"));
            }
            try
            {
                DozenRouletteBet bet = new(testBet);
                bet.SetDozen((BetDozenType)64);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Invalid dozen argument"));
            }
        }
    }
}