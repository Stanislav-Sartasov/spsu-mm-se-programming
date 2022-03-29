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
            RouletteBet bet = new(testBet, BetType.Color);
            bet.SetColor(0);
            roulette.Play(bet);
            Assert.IsTrue(roulette.LastNumber >= 0 && roulette.LastNumber <= 36);
        }

        [Test]
        public void TestGetColorCode()
        {
            Assert.IsTrue(roulette.GetColorCode(1) == 1);
            Assert.IsTrue(roulette.GetColorCode(2) == 0);
            Assert.IsTrue(roulette.GetColorCode(0) == 2);
            try
            {
                roulette.GetColorCode(999);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Ошибка! Тип ставки за пределом разрешённых значений."));
            }
        }

        [Test]
        public void TestColorBet()
        {
            Int64 testColor = 0;
            RouletteBet bet = new(testBet, BetType.Color);
            bet.SetColor(testColor);
            Int64 result = roulette.Play(bet);

            if (roulette.GetColorCode(roulette.LastNumber) == testColor)
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
            RouletteBet bet = new(testBet, BetType.Single);
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
            Int64 testDozen = 1;
            RouletteBet bet = new(testBet, BetType.Dozen);
            bet.SetDozen(testDozen);
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
            Int64 testNumber = 1;
            RouletteBet bet = new(testBet, BetType.Parity);
            bet.SetParity(testNumber);
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
                RouletteBet bet = new(testBet, (CasinoLib.BetType)55);
                roulette.Play(bet);
                result = roulette.Play(bet);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Bet type is out of allowed range"));
            }
            try
            {
                RouletteBet bet = new(testBet, BetType.Single);
                bet.SetSingle(64);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Argument can be between 0 and 36"));
            }
            try
            {
                RouletteBet bet = new(testBet, BetType.Color);
                bet.SetColor(64);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Argument can be between 0 and 2"));
            }
            try
            {
                RouletteBet bet = new(testBet, BetType.Parity);
                bet.SetParity(64);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Argument can be either 0 or 1"));
            }
            try
            {
                RouletteBet bet = new(testBet, BetType.Dozen);
                bet.SetDozen(64);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Argument can be between 1 and 3"));
            }
        }
    }
}