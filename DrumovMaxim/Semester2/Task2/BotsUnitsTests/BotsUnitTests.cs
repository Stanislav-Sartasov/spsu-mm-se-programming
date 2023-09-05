using NUnit.Framework;
using GameTable;
using GameTable.BotStructure;
using GameTable.BetsType;
using GameTable.SectorTypeEnum;

namespace Bots.UnitsTests
{
    public class BotsUnitTests
    {

        [Test]
        public void CreateBotsTest()
        {
            IBot WideStrideBot = new WideStrideBot("WideStrideBot", 10000, 40);
            IBot ThomasDonaldBot = new ThomasDonaldBot("ThomasDonaldBot", 30000, 40);
            IBot ProgressionSeriesBot = new ProgressionSeriesBot("ProgressionSeriesBot", 10000, 40);

            Assert.AreEqual(WideStrideBot.Name, "WideStrideBot");
            Assert.AreEqual(ProgressionSeriesBot.Name, "ProgressionSeriesBot");
            Assert.AreEqual(ThomasDonaldBot.Name, "ThomasDonaldBot");

            Assert.AreEqual(WideStrideBot.Balance, 10000);
            Assert.AreEqual(ProgressionSeriesBot.Balance, 10000);
            Assert.AreEqual(ThomasDonaldBot.Balance, 30000);

            Assert.AreEqual(WideStrideBot.GameCounter, 40);
            Assert.AreEqual(ProgressionSeriesBot.GameCounter, 40);
            Assert.AreEqual(ThomasDonaldBot.GameCounter, 40);
        }

        [Test]
        public void BetsTest()
        {
            SectorType winningSector = new SectorType(15, ColourEnum.Black, ParityEnum.Odd, DozenEnum.SecondDozen);
            SectorType lostSector = new SectorType(34, ColourEnum.Red, ParityEnum.Even, DozenEnum.ThirdDozen);

            NumberBet numBet = new NumberBet(1000, 15);
            Assert.AreEqual(numBet.value, 1000);
            Assert.AreEqual(numBet.SelectedSector, 15);
            Assert.AreEqual(numBet.ReviewBet(lostSector), 0);
            Assert.AreEqual(numBet.ReviewBet(winningSector), 36);

            ColourBet clrBet = new ColourBet(993, ColourEnum.Black);
            Assert.AreEqual(clrBet.value, 993);
            Assert.AreEqual(clrBet.SelectedSector, ColourEnum.Black);
            Assert.AreEqual(clrBet.ReviewBet(lostSector), 0);
            Assert.AreEqual(clrBet.ReviewBet(winningSector), 2);

            DozenBet dznBet = new DozenBet(985, DozenEnum.SecondDozen);
            Assert.AreEqual(dznBet.value, 985);
            Assert.AreEqual(dznBet.SelectedSector, DozenEnum.SecondDozen);
            Assert.AreEqual(dznBet.ReviewBet(lostSector), 0);
            Assert.AreEqual(dznBet.ReviewBet(winningSector), 3);

            ParityBet parBet = new ParityBet(979, ParityEnum.Odd);
            Assert.AreEqual(parBet.value, 979);
            Assert.AreEqual(parBet.SelectedSector, ParityEnum.Odd);
            Assert.AreEqual(parBet.ReviewBet(lostSector), 0);
            Assert.AreEqual(parBet.ReviewBet(winningSector), 2);

        }

        [Test]
        public void GameWideStrideBotTest()
        {
            IBot WideStrideBot = new WideStrideBot("WideStrideBot", 10000, 40);
            Roulette roulette = new Roulette(WideStrideBot);
            
            roulette.MakeBets();
            Assert.AreEqual(WideStrideBot.LastBet, 100);

            roulette.LaunchGame();
            roulette.MakeBets();
            if (WideStrideBot.LastGame)
            {
                Assert.AreEqual(WideStrideBot.LastGame, true);
                Assert.AreEqual(WideStrideBot.LastBet, 100);
            }
            else
            {
                Assert.AreEqual(WideStrideBot.LastGame, false);
                Assert.AreEqual(WideStrideBot.LastBet, 200);
            }
            WideStrideBot.Balance = 0;
            roulette.MakeBets();
            Assert.AreEqual(WideStrideBot.State, BotState.Stop);
        }

        [Test]
        public void GameThomasDonaldBotTest()
        {
            IBot ThomasDonaldBot = new ThomasDonaldBot("ThomasDonaldBot", 10000, 40);
            Roulette roulette = new Roulette(ThomasDonaldBot);

            roulette.MakeBets();
            Assert.AreEqual(ThomasDonaldBot.LastBet, 100);
            
            roulette.LaunchGame();
            roulette.MakeBets();
            if (ThomasDonaldBot.LastGame)
            {
                Assert.AreEqual(ThomasDonaldBot.LastGame, true);
                Assert.AreEqual(ThomasDonaldBot.LastBet, 100);
            }
            else
            {
                Assert.AreEqual(ThomasDonaldBot.LastGame, false);
                Assert.AreEqual(ThomasDonaldBot.LastBet, 200);
            }
            ThomasDonaldBot.Balance = 0;
            roulette.MakeBets();
            Assert.AreEqual(ThomasDonaldBot.State, BotState.Stop);
        }

        [Test]
        public void GameProgressionSeriesBotTest()
        {
            IBot ProgressionSeriesBot = new ProgressionSeriesBot("ProgressionSeriesBot", 10000, 40);
            Roulette roulette = new Roulette(ProgressionSeriesBot);

            roulette.MakeBets();
            Assert.AreEqual(ProgressionSeriesBot.LastBet, 100);

            roulette.LaunchGame();
            roulette.MakeBets();
            if (ProgressionSeriesBot.LastGame)
            {
                Assert.AreEqual(ProgressionSeriesBot.LastGame, true);
                Assert.AreEqual(ProgressionSeriesBot.LastBet, 300);
            }
            else
            {
                Assert.AreEqual(ProgressionSeriesBot.LastGame, false);
                Assert.AreEqual(ProgressionSeriesBot.LastBet, 100);
            }
            ProgressionSeriesBot.Balance = 0;
            roulette.MakeBets();
            Assert.AreEqual(ProgressionSeriesBot.State, BotState.Stop);
        }
        [Test]
        public void GameOtherNameBotTest()
        {
            IBot ProgressionSeriesBot = new ProgressionSeriesBot("OtherName", 10000, 40);
            Roulette roulette = new Roulette(ProgressionSeriesBot);

            roulette.MakeBets();
            Assert.AreEqual(ProgressionSeriesBot.LastBet, 100);

            roulette.LaunchGame();
            roulette.MakeBets();
            if (ProgressionSeriesBot.LastGame)
            {
                Assert.AreEqual(ProgressionSeriesBot.LastGame, true);
                Assert.AreEqual(ProgressionSeriesBot.LastBet, 300);
            }
            else
            {
                Assert.AreEqual(ProgressionSeriesBot.LastGame, false);
                Assert.AreEqual(ProgressionSeriesBot.LastBet, 100);
            }
            ProgressionSeriesBot.Balance = 0;
            roulette.MakeBets();
            Assert.AreEqual(ProgressionSeriesBot.State, BotState.Stop);
        }
    }
}