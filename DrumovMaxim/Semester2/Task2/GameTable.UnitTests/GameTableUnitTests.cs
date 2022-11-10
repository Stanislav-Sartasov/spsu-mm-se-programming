using NUnit.Framework;
using Bots;
using Task2;
using GameTable.BotStructure;
using GameTable.SectorTypeEnum;

namespace GameTable.UnitTests
{
    public class GameTableUnitTests
    {
        ThomasDonaldBot testBot = new ThomasDonaldBot("ThomasDonaldBot", 10000, 40);

        [Test]
        public void SectorTypeTest()
        {
            SectorType sector = new SectorType(32, ColourEnum.Red, ParityEnum.Even, DozenEnum.ThirdDozen);

            Assert.AreEqual(sector.Number, 32);
            Assert.AreEqual(sector.Colour, ColourEnum.Red);
            Assert.AreEqual(sector.Parity, ParityEnum.Even);
            Assert.AreEqual(sector.Dozen, DozenEnum.ThirdDozen);
        }
        [Test]
        public void CreateRouletteTest()
        {
            Roulette roulette = new Roulette(testBot);
            roulette.Game();

            Assert.AreEqual(BotState.Stop, testBot.State);

            Assert.AreEqual(roulette.Sectors[0].Colour, null);
            Assert.AreEqual(roulette.Sectors[0].Parity, null);
            Assert.AreEqual(roulette.Sectors[0].Dozen, null);
            
            Assert.AreEqual(roulette.Sectors[1].Colour, ColourEnum.Red);
            Assert.AreEqual(roulette.Sectors[1].Parity, ParityEnum.Even);
            Assert.AreEqual(roulette.Sectors[1].Dozen, DozenEnum.ThirdDozen);

            Assert.AreEqual(roulette.Sectors[15].Colour, ColourEnum.Red);
            Assert.AreEqual(roulette.Sectors[15].Parity, ParityEnum.Even);
            Assert.AreEqual(roulette.Sectors[15].Dozen, DozenEnum.ThirdDozen);

            Assert.AreEqual(roulette.Sectors[25].Colour, ColourEnum.Red);
            Assert.AreEqual(roulette.Sectors[25].Parity, ParityEnum.Even);
            Assert.AreEqual(roulette.Sectors[25].Dozen, DozenEnum.SecondDozen);

        }
        [Test]
        public void LaunchCasinoTest()
        {
            StartCasino startCasinoTest = new StartCasino(testBot, "TestName");
            int startBalance = testBot.Balance;
            startCasinoTest.Launch();

            Assert.AreEqual(startCasinoTest.name, "TestName");
            Assert.AreEqual(startCasinoTest.bot, testBot);
            Assert.AreEqual(testBot.Gain, testBot.Balance - startBalance);
            if(testBot.GameCounter + 1 == 0)
            {
                Assert.AreEqual(testBot.GameCounter + 1, 0);
            }
            Assert.AreEqual(testBot.State, BotState.Stop);

        }

    }
}