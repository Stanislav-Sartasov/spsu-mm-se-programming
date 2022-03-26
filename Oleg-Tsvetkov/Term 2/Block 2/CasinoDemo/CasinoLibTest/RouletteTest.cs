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
            roulette.Play(BetType.Color, 0, 10);
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
            Int64 result = roulette.Play(BetType.Color, testColor, testBet);

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
            Int64 result = roulette.Play(BetType.Single, testNumber, testBet);

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
            Int64 result = roulette.Play(BetType.Dozen, testDozen, testBet);

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
            Int64 result = roulette.Play(BetType.Parity, testNumber, testBet);

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
        public void TestPlayExceptions()
        {
            Int64 result;
            try
            {
                result = roulette.Play((CasinoLib.BetType)55, 1, testBet);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Ошибка! Тип ставки за пределом разрешённых значений."));
            }
            try
            {
                result = roulette.Play(BetType.Single, 64, testBet);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Ошибка! При ставке на одно число выбор должен быть от 0 до 36."));
            }
            try
            {
                result = roulette.Play(BetType.Color, 64, testBet);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Ошибка! При ставке на цвет число выбор должен равняться 1 или 0."));
            }
            try
            {
                result = roulette.Play(BetType.Parity, 64, testBet);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Ошибка! При ставке на чётность выбор должен равняться 1 или 0."));
            }
            try
            {
                result = roulette.Play(BetType.Dozen, 64, testBet);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Ошибка! При ставке на дюжину выбор должен быть от 1 до 3."));
            }
        }
    }
}