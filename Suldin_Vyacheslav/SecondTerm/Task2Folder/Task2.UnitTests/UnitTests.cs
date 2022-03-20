using NUnit .Framework;
using System.IO;
using System.Linq;
using BasicLibraly;
using System.Collections.Generic;
using BotLibraly;

namespace Task2.UnitTests
{
    public class UnitTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]

        public void CalculateBetsTest()
        {
            Gamester gamester = new Gamester();
            List<Gamester> gamesters = new List<Gamester> { gamester };
            Diler diler = new Diler();


            for (int i = 3; i < 30; i++)
            {
                for (int j = 3; j < 30; j++)
                {
                    gamester.Bank = 0;
                    gamester.Bets[0] = 100;
                    gamester.Sum[0] = i;
                    diler.Sum[0] = j;
                    diler.CalculateBets(gamesters);

                    if (gamester.Bets[0] != 0) Assert.Fail();

                    switch (gamester.Bank)
                    {
                        case 0:
                            {
                                if (j < i && i < 21) Assert.Fail();
                                break;
                            }
                        case 100:
                            {
                                if (i != j) Assert.Fail();
                                break;
                            }
                        case 200:
                            {
                                if ((i < j && j < 21) || i > 21) Assert.Fail();
                                break;
                            }
                        default:
                            {
                                Assert.Fail();
                                break;
                            }
                    }

                }
            }

            gamester.Sum[0] = 0;
            diler.GiveBlackJack(gamester, 0);

            for (int i = 2; i < 21; i++)
            {
                diler.Sum[0] = i;
                gamester.Bank = 0;
                gamester.Bets[0] = 100;
                diler.CalculateBets(gamesters);
                if ((gamester.Bank != 250 || gamester.Bets[0] != 0) && i != 11) Assert.Fail();
            }

            diler.Sum[0] = 0;
            diler.GiveBlackJack(diler, 0);

            gamester.Bank = 0;
            gamester.Bets[0] = 100;
            diler.CalculateBets(gamesters);
            if (gamester.Bank != 100 || gamester.Bets[0] != 0) Assert.Fail();

            diler.GetCardsBack(gamesters);
            diler.GiveBlackJack(diler, 0);

            for (int i = 2; i < 21; i++)
            {
                gamester.Sum[0] = i;
                gamester.Bank = 0;
                gamester.Bets[0] = 100;
                diler.CalculateBets(gamesters);
                if (gamester.Bank != 0 || gamester.Bets[0] != 0) Assert.Fail(i.ToString());
            }
            Assert.Pass();
        }


        [Test]

        public void BotAnswerTest()
        {
            Diler diler = new Diler();
            for (int str = 1; str < 4; str++)
            {
                Shoes shoes = new Shoes();
                Bot bot = new Bot(str);
                List<Gamester> gamesters = new List<Gamester> { bot };
                string[] testSet = new string[] {"Ace","2","3","4", "5", "6", "7", "8",
                    "9", "10", "Jack", "Queen", "King"};

                for (int j = 0; j < testSet.Length; j++)
                    for (int k = 0; k < testSet.Length; k++)
                    {
                        bot.ReceiveCard(new Card("diamonds", testSet[j]), 0);
                        bot.ReceiveCard(new Card("diamonds", testSet[k]), 0);
                        for (int i = 0; i < testSet.Length; i++)
                            if (bot.Answer(0, new List<Card> { new Card("diamonds", testSet[i]) }, gamesters, shoes) == -1)
                                Assert.Fail(testSet[i], testSet[j], testSet[k]);
                        diler.GetCardsBack(gamesters);
                    }
                Assert.Pass();
            }
        }

        [Test]
        public void ReceiveCardTest()
        {
            Diler diler = new Diler();
            string[] testSet = new string[] {"Ace","2","3","4", "5", "6", "7", "8",
                "9", "10", "Jack", "Queen", "King"};

            string[] suitSet = new string[] { "Clubs", "Diamonds", "Hearts", "Spades" };

            Gamester gamester = new Gamester();
            for (int j = 0; j < suitSet.Length; j++)
            {
                for (int i = 0; i < testSet.Length; i++)
                {
                    Card testCard = new Card(suitSet[j], testSet[i]);
                    gamester.ReceiveCard(testCard, 0);
                    if (gamester.Hands[0][0].Rank != testSet[i] ||
                    gamester.Hands[0][0].Suit != suitSet[j] ||
                    (gamester.Sum[0] != i + 1 && i < 10) ||
                    (gamester.Sum[0] != 10 && i >= 9) ||
                    gamester.Hands[0][0].Value != gamester.Sum[0])
                    {
                        Assert.Fail();
                    }
                    diler.GetCardsBack(new List<Gamester> { gamester });
                }
            }

            Assert.Pass();
        }

        [Test]

        public void GetCardsBackTest()
        {
            Diler diler = new Diler();
            Gamester gamester = new Gamester();
            List<Gamester> gamesters = new List<Gamester>() { gamester };
            Deck deck = new Deck();
            for (int k = 0; k < gamesters.Count; k++)
                for (int j = 0; j < 4; j++)
                    for (int i = 0; i < 50; i += 10)
                        gamester.ReceiveCard(deck.Cards[i], j);

            diler.GetCardsBack(gamesters);
            for (int k = 0; k < gamesters.Count; k++)
                for (int j = 0; j < 4; j++)
                    if (gamesters[k].Hands[j].Count != 0)
                        Assert.Fail();

            Assert.Pass();
        }

        [Test]

        public void IsBlackJackTest()
        {
            Card[] testSet = new Card[] {new Card("Clubs","Ace"), new Card("Clubs", "2"), new Card("Clubs", "3"),
            new Card("Clubs","4"),new Card("Clubs","5"),new Card("Clubs","6"),new Card("Clubs","7"),
            new Card("Clubs","8"),new Card("Clubs","9"),new Card("Clubs","10"),new Card("Clubs","Jack"),
            new Card("Clubs","King"),new Card("Clubs","Queen")};

            Player player = new Player();
            Diler diler = new Diler();
            for (int i = 0; i < testSet.Length; i++)
            {
                for (int j = 0; j < testSet.Length; j++)
                {
                    for (int k = 0; k < testSet.Length; k++)
                    {
                        player.ReceiveCard(testSet[i], 0);
                        if (player.IsBlackJack(0)) Assert.Fail();
                        player.ReceiveCard(testSet[j], 0);
                        if ((player.Sum[0] != 11)
                            && player.IsBlackJack(0)) Assert.Fail();
                        player.ReceiveCard(testSet[k], 0);
                        if (player.IsBlackJack(0)) Assert.Fail();
                    }
                }
            }
            Assert.Pass();
        }

        [Test]

        public void GiveBlackJackTest()
        {
            Player player = new Player();
            Diler diler = new Diler();
            diler.GiveBlackJack(player, 0);
            if (!player.IsBlackJack(0)) Assert.Fail();

        }

        [Test]
        public void CalculateValueTest()
        {
            string[] testSet = new string[] {"Ace","2","3","4", "5", "6", "7", "8",
                "9", "10", "Jack", "Queen", "King"};
            int result = 1;


            foreach (string cardRank in testSet)
            {
                Card testCard = new Card("diamonds", cardRank);

                testCard.Value = testCard.CalculateValue(testCard.Rank);
                if (testCard.Value != result)
                    Assert.Fail();

                if (result < 10)
                    result++;
            }

            Assert.Pass();
        }

        [Test]
        public void GameTest()
        {
            List<Gamester> testPlayers = new List<Gamester> { new Gamester() };
            Game testGame = new Game(testPlayers);
            if (testGame.TheDiler == null ||
                testGame.TheShoes == null ||
                testPlayers[0] != testGame.Gamesters[0]) Assert.Fail();

            testGame.Start(40);
            Assert.Pass();
        }

        [Test]

        public void BotMakeBetTest()
        {
            Bot justPlayer = new Bot(1);
            OneThreeTwoSix testBot = new OneThreeTwoSix(1);
            Martingale marty = new Martingale(1);
            Counter counter = new Counter(1);
            Bot[] botSet = new Bot[] { testBot, marty, counter };
            for (int j = 0; j < 4; j++)
            {
                justPlayer.Bank = 10000;
                justPlayer.MakeBet(j);
                if (justPlayer.Bets[j] != 100 || justPlayer.Bank != 10000 - 100) Assert.Fail();
                for (int loseStr = 0; loseStr < 5; loseStr++) 
                {
                    marty.Bank = 10000;
                    marty.LastBank = marty.Bank + 1;
                    marty.LoseStreak = loseStr;
                    marty.MakeBet(j);
                    
                    switch (loseStr)
                    {
                        case 0:
                            {
                                if (marty.Bank != 9800 | marty.Bets[j] != 200) Assert.Fail();
                                break;
                            }
                        case 1:
                            {
                                if (marty.Bank != 9600 | marty.Bets[j] != 400) Assert.Fail();
                                break;
                            }
                        case 2:
                            {
                                if (marty.Bank != 9200 | marty.Bets[j] != 800) Assert.Fail();
                                break;
                            }
                        case 3:
                            {
                                if (marty.Bank != 8400 | marty.Bets[j] != 1600) Assert.Fail();
                                break;
                            }
                        case 4:
                            {
                                if (marty.Bank != 6800 | marty.Bets[j] != 3200) Assert.Fail();
                                break;
                            }
                        default:
                            {
                                if (marty.Bank != 3600 | marty.Bets[j] != 6400) Assert.Fail();
                                break;

                            }
                    }
                }
                for (int step = 0; step < 4; step++)
                {
                    testBot.Bank = 10000;
                    testBot.Step = step;
                    testBot.LastBank = 0;
                    testBot.MakeBet(j);
                    switch (step)
                    {
                        case 0:
                            {
                                if (testBot.Bets[j] != 50 || testBot.Bank != 9950) Assert.Fail("1");
                                break;
                            }
                        case 1:
                            {
                                if (testBot.Bank != 9850 || testBot.Bets[j] != 150) Assert.Fail("2");
                                break;
                            }
                        case 2:
                            {
                                if (testBot.Bets[j] != 100 || testBot.Bank != 9900) Assert.Fail("3");
                                break;
                            }
                        case 3:
                            {
                                if (testBot.Bank != 9700 | testBot.Bets[j] != 300) Assert.Fail("4");
                                break;
                            }
                        default:
                            {
                                if (testBot.Step != 0 || testBot.Bets[j]!=50 ||testBot.Bank!=9950) Assert.Fail("5");
                                break;
                            }
                    }
                }

                counter.Bank = 90;
                counter.MakeBet(j);
                if (counter.Bank!=0 || counter.Bets[j]!=90) Assert.Fail("1");
                counter.Bank = 200;
                counter.MakeBet(j);
                if (counter.Bank!=100|| counter.Bets[j]!=100) Assert.Fail("2");
            }
            Assert.Pass();
        }

        [Test]

        public void CounterAnswerTest()
        {
            Bot bot = new Bot(1);
            Counter counter = new Counter(1);
            Shoes shoes = new Shoes();
            Diler diler = new Diler();
            diler.FillShoe(shoes, 8);
            for (int i = 1; i < 100; i++)
            {
                shoes.Current = i;
                bot.ReceiveCard(shoes.Queue[shoes.Current-1], 0);
                bot.ReceiveCard(shoes.Queue[shoes.Current], 0);
                counter.ReceiveCard(shoes.Queue[shoes.Current-1], 0);
                counter.ReceiveCard(shoes.Queue[shoes.Current], 0);
                diler.ReceiveCard(shoes.Queue[shoes.Current + 1], 0);
                int answer = counter.Answer(0, diler.Hands[0], new List<Gamester> { bot, counter }, shoes);
                if (answer != bot.Answer(0, diler.Hands[0], new List<Gamester> { bot, counter }, shoes) &&
                   ((answer == 2 && counter.CounterWork(shoes) < 20) || (answer == 0 && counter.CounterWork(shoes) > 20)) )
                   Assert.Fail();
            }

            Assert.Pass();
        }

        [Test]


        public void AskTest()
        {
            Shoes shoes = new Shoes();
            Diler diler = new Diler();
            diler.FillShoe(shoes, 8);
            Gamester player = new Gamester();
            List<Gamester> testPlayers = new List<Gamester> { player };
            int[,] testCondition = new int[1, 4] { { 0, 0, 0, 0 } };
            for (int j = 0; j<3;j++)
                for (int i = 0; i <= 4; i++)
                {
                    testCondition[0, j] = i;
                    player.Bank = 1000;
                    player.ReceiveCard(new Card("Clubs", "10"), j);
                    for (int k = 0; k < 4;k++) player.Bets[k] = i * 100;

                    switch (i)
                    {
                        case 0:
                            {
                            
                                diler.Ask(testPlayers, shoes, testCondition);
                                if (player.Hands[j].Count != 1 ||
                                     player.Bank != 1000 - i * 100 ||
                                     player.Bets[j] != i*100) Assert.Fail("1");
                                    break;
                            }
                        case 1:
                            {
                                diler.Ask(testPlayers, shoes, testCondition);
                                if (player.Hands[j].Count != 2 ||
                                     player.Bank != 1000 ||
                                     player.Bets[j] != i * 100) Assert.Fail("2");
                                break;
                            }
                        case 2:
                            {
                                diler.Ask(testPlayers, shoes, testCondition);
                                if (player.Hands[j].Count != 2 ||
                                     player.Bank != 1000 -  i * 100 ||
                                     player.Bets[j] != i * 200) Assert.Fail("3");

                                break;
                            }
                        case 3:
                            {
                                player.ReceiveCard(new Card("Hearts", "10"), j);
                                diler.Ask(testPlayers, shoes, testCondition);
                                if (player.Bank != 1000 - i * 100 ||
                                    player.Bets[j + 1] != i * 100 ||
                                    player.Hands[j][0] == new Card("Clubs", "10") ||
                                    player.Hands[j + 1][0] == new Card("Hearts", "10") ||
                                    player.Hands[j].Count != player.Hands[j + 1].Count) Assert.Fail("4");
                                break;
                            }
                        default:
                            {
                                diler.Ask(testPlayers, shoes, testCondition);
                                if (player.Hands[j].Count != 1 ||
                                     player.Bank != 1000 + i * 50 ||
                                     player.Bets[j] != 0) Assert.Fail("5");
                                break;
                            }
                            
                    }
                    diler.GetCardsBack(testPlayers);
                }
            Assert.Pass();

        }

        [Test]

        public void EqualCardTest()
        {
            string[] suitSet = new string[] { "Clubs", "Diamonds", "Hearts", "Spades" };
            string[] testSet = new string[] {"Ace","2","3","4", "5", "6", "7", "8",
                "9", "10", "Jack", "Queen", "King"};


            for (int i = 0; i < suitSet.Length; i++)
                for (int j = 0; j < suitSet.Length; j++)
                    for (int k = 0; k < testSet.Length; k++)
                        for (int p = 0; p < testSet.Length; p++)
                        {
                            Card firstCard = new Card(suitSet[i], testSet[k]);
                            Card secondCard = new Card(suitSet[j], testSet[p]);
                            if (firstCard == secondCard && (i != j || k != p)) Assert.Fail(suitSet[i]+suitSet[j]+ testSet[k] + testSet[p]);
                            if (firstCard != secondCard && (i == j && k == p)) Assert.Fail();
                         }
            Assert.Pass();
        }


    }
}