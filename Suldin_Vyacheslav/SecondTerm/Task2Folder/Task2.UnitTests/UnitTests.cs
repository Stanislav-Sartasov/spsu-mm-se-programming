using NUnit .Framework;
using System.IO;
using System.Linq;
using BasicLibrary;
using System.Collections.Generic;
using BotLibrary;
using System;

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
            Dealer dealer = new Dealer();


            for (int i = 3; i < 30; i++)
            {
                for (int j = 3; j < 30; j++)
                {
                    gamester.Bank = 0;
                    gamester.Bets[0] = 100;
                    gamester.Sum[0] = i;
                    dealer.Sum[0] = j;
                    dealer.CalculateBets(gamesters);

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
            dealer.GiveBlackJack(gamester, 0);

            for (int i = 2; i < 21; i++)
            {
                dealer.Sum[0] = i;
                gamester.Bank = 0;
                gamester.Bets[0] = 100;
                dealer.CalculateBets(gamesters);
                if ((gamester.Bank != 250 || gamester.Bets[0] != 0) && i != 11) Assert.Fail();
            }

            dealer.Sum[0] = 0;
            dealer.GiveBlackJack(dealer, 0);

            gamester.Bank = 0;
            gamester.Bets[0] = 100;
            dealer.CalculateBets(gamesters);
            if (gamester.Bank != 100 || gamester.Bets[0] != 0) Assert.Fail();

            dealer.GetCardsBack(gamesters);
            dealer.GiveBlackJack(dealer, 0);

            for (int i = 2; i < 21; i++)
            {
                gamester.Sum[0] = i;
                gamester.Bank = 0;
                gamester.Bets[0] = 100;
                dealer.CalculateBets(gamesters);
                if (gamester.Bank != 0 || gamester.Bets[0] != 0) Assert.Fail(i.ToString());
            }
            Assert.Pass();
        }


        [Test]

        public void BotAnswerTest()
        {
            Dealer dealer = new Dealer();
            for (int str = 1; str < 4; str++)
            {
                Shoes shoes = new Shoes();
                Bot bot = new Bot(str);
                List<Gamester> gamesters = new List<Gamester> { bot };

                for (int j = 1; j < 14; j++)
                    for (int k = 1; k < 14; k++)
                    {
                        bot.ReceiveCard(new Card(1, j), 0);
                        bot.ReceiveCard(new Card(1, k), 0);
                        for (int i = 1; i < 14; i++)
                            if (bot.Answer(0, new List<Card> { new Card(1, i) }, gamesters) == -1)
                                Assert.Fail();
                        dealer.GetCardsBack(gamesters);
                    }
                Assert.Pass();
            }
        }

        [Test]
        public void ReceiveCardTest()
        {
            Dealer dealer = new Dealer();

            Gamester gamester = new Gamester();
            for (int j = 1; j < 5; j++)
            {
                for (int i = 1; i < 14; i++)
                {
                    Card testCard = new Card(j, i);
                    gamester.ReceiveCard(testCard, 0);
                    if (gamester.Hands[0][0].GetCardInfo()[0] != i ||
                    gamester.Hands[0][0].GetCardInfo()[1] != j ||
                    (gamester.Sum[0] != i && i <= 10) ||
                    (gamester.Sum[0] != 10 && i > 10) ||
                    gamester.Hands[0][0].GetCardInfo()[2] != gamester.Sum[0])
                    {
                        Assert.Fail(i.ToString());
                    }
                    dealer.GetCardsBack(new List<Gamester> { gamester });
                }
            }

            Assert.Pass();
        }

        [Test]

        public void GetCardsBackTest()
        {
            Dealer dealer = new Dealer();
            Gamester gamester = new Gamester();
            List<Gamester> gamesters = new List<Gamester>() { gamester };
            Deck deck = new Deck();
            for (int k = 0; k < gamesters.Count; k++)
                for (int j = 0; j < 4; j++)
                    for (int i = 0; i < 50; i += 10)
                        gamester.ReceiveCard(deck.GetCards()[i], j);

            dealer.GetCardsBack(gamesters);
            for (int k = 0; k < gamesters.Count; k++)
                for (int j = 0; j < 4; j++)
                    if (gamesters[k].Hands[j].Count != 0)
                        Assert.Fail();

            Assert.Pass();
        }

        [Test]

        public void IsBlackJackTest()
        {
            Card[] testSet = new Card[13];

            for (int i = 1; i < 14; i++)
            {
                testSet[i - 1] = new Card(1, i);
            }

            Player player = new Player();
            Dealer dealer = new Dealer();
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
            Dealer dealer = new Dealer();
            dealer.GiveBlackJack(player, 0);
            if (!player.IsBlackJack(0)) Assert.Fail();

        }

        [Test]
        public void CalculateValueTest()
        {
            
            int result = 1;


            for (int i = 1; i < 14; i++)
            {
                Card testCard = new Card(1, i);

                testCard.GetCardInfo()[2] = testCard.CalculateValue(testCard.GetCardInfo()[0]);
                if (testCard.GetCardInfo()[2] != result)
                    Assert.Fail();

                if (result < 10)
                    result++;
            }

            Assert.Pass();
        }

        
        [Test]

        public void BotMakeBetTest()
        {
            Bot justPlayer = new Bot(1);
            OneThreeTwoSix testBot = new OneThreeTwoSix(1);
            Martingale marty = new Martingale(1);
            Oscar oscar = new Oscar(1);
            Bot[] botSet = new Bot[] { testBot, marty, oscar };
            for (int j = 0; j < 4; j++)
            {
                justPlayer.Bank = 10000;
                justPlayer.MakeBet(j);
                if (justPlayer.Bets[j] != 100 || justPlayer.Bank != 10000 - 100) Assert.Fail();
                for (int k = 0; k < 5; k++) 
                {
                    marty.Bank = 10000;
                    marty.LastBank = marty.Bank + 1;
                    marty.LoseStreak = k;
                    marty.MakeBet(j);

                    testBot.Bank = 10000;
                    testBot.Step = k;
                    testBot.LastBank = 0;
                    testBot.MakeBet(j);

                    oscar.Wins = k;
                    oscar.Bank = 10000;
                    oscar.LastBank = 10001;
                    oscar.MakeBet(j);

                    switch (k)
                    {
                        case 0:
                            {
                                if (marty.Bank != 9800 || marty.Bets[j] != 200
                                    || testBot.Bets[j] != 50 || testBot.Bank != 9950
                                    || oscar.Bets[j] != 100 || oscar.Bank != 9900) Assert.Fail();
                                break;
                            }
                        case 1:
                            {
                                if (marty.Bank != 9600 || marty.Bets[j] != 400
                                    || testBot.Bank != 9850 || testBot.Bets[j] != 150
                                    || oscar.Bets[j] != 103 || oscar.Bank != 9897) Assert.Fail();
                                break;
                            }
                        case 2:
                            {
                                if (marty.Bank != 9200 || marty.Bets[j] != 800
                                    || testBot.Bets[j] != 100 || testBot.Bank != 9900
                                    || oscar.Bets[j] != 106 || oscar.Bank != 9894) Assert.Fail();
                                break;
                            }
                        case 3:
                            {
                                if (marty.Bank != 8400 || marty.Bets[j] != 1600
                                    || testBot.Bank != 9700 | testBot.Bets[j] != 300
                                    || oscar.Bets[j] != 109 || oscar.Bank != 9891) Assert.Fail();
                                break;
                            }
                        case 4:
                            {
                                if (marty.Bank != 6800 || marty.Bets[j] != 3200
                                    || testBot.Step != 0 || testBot.Bets[j] != 50 || testBot.Bank != 9950
                                    || oscar.Bets[j] != 112 || oscar.Bank != 9888) Assert.Fail();
                                break;
                            }
                        default:
                            {
                                if (marty.Bank != 3600 | marty.Bets[j] != 6400) Assert.Fail();
                                break;

                            }
                    }
                }
            }
            Assert.Pass();
        }

        [Test]


        public void AskTest()
        {
            Shoes shoes = new Shoes();
            Dealer dealer = new Dealer();
            shoes.Fill(8);
            Gamester player = new Gamester();
            List<Gamester> testPlayers = new List<Gamester> { player };
            int[,] testCondition = new int[1, 4] { { 0, 0, 0, 0 } };
            for (int j = 0; j < 3; j++)
                for (int i = 0; i <= 4; i++)
                {
                    testCondition[0, j] = i;
                    player.Bank = 1000;
                    player.ReceiveCard(new Card(1, 10), j);
                    for (int k = 0; k < 4; k++) player.Bets[k] = i * 100;

                    switch (i)
                    {
                        case 0:
                            {

                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player.Hands[j].Count != 1 ||
                                     player.Bank != 1000 - i * 100 ||
                                     player.Bets[j] != i * 100) Assert.Fail("1");
                                break;
                            }
                        case 1:
                            {
                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player.Hands[j].Count != 2 ||
                                     player.Bank != 1000 ||
                                     player.Bets[j] != i * 100) Assert.Fail("2");
                                break;
                            }
                        case 2:
                            {
                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player.Hands[j].Count != 2 ||
                                     player.Bank != 1000 - i * 100 ||
                                     player.Bets[j] != i * 200) Assert.Fail("3");

                                break;
                            }
                        case 3:
                            {
                                player.ReceiveCard(new Card(2, 10), j);
                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player.Bank != 1000 - i * 100 ||
                                    player.Bets[j + 1] != i * 100 ||
                                    player.Hands[j][0] == new Card(1, 10) ||
                                    player.Hands[j + 1][0] == new Card(2, 10) ||
                                    player.Hands[j].Count != player.Hands[j + 1].Count) Assert.Fail("4");
                                break;
                            }
                        default:
                            {
                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player.Hands[j].Count != 1 ||
                                     player.Bank != 1000 + i * 50 ||
                                     player.Bets[j] != 0) Assert.Fail("5");
                                break;
                            }

                    }
                    dealer.GetCardsBack(testPlayers);
                }
            Assert.Pass();

        }

        [Test]

        public void EqualCardTest()
        {
            for (int i = 1; i < 5; i++)
                for (int j = 1; j < 5; j++)
                    for (int k = 1; k < 14; k++)
                        for (int p = 1; p < 14; p++)
                        {
                            Card firstCard = new Card(i, k);
                            Card secondCard = new Card(j, p);
                            if (firstCard == secondCard && (i != j || k != p)) Assert.Fail();
                            if (firstCard != secondCard && (i == j && k == p)) Assert.Fail();
                         }
            Assert.Pass();
        }


        [Test]

        public void GameTest()
        {
            Bot[] set = new Bot[] { new Oscar(1), new OneThreeTwoSix(1), new Martingale(1),
            new Oscar(2), new OneThreeTwoSix(2), new Martingale(2),
            new Oscar(3), new OneThreeTwoSix(3), new Martingale(3)};
            List<Gamester> players = new List<Gamester>(10);
            players.AddRange(set);
            Game jackBlack = new Game(players);
            try
            {
                for (int i = 0; i < set.Length; i++)
                    set[i].Bank = 100000;
                jackBlack.Start(100);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
        [Test]

        public void ShowTableTest()
        {
            Dealer dealer = new Dealer();
            List<Gamester> gamesters = new List<Gamester>(10);
            int[,] condition = new int[10, 4];

            Shoes shoes = new Shoes();
            shoes.Fill(8);

            for (int i = 0; i < 10; i++)
                gamesters.Add(new Gamester());
            
            Game JackBlack = new Game(gamesters);
            for (int i = 0; i < 10; i++)
            {
                dealer.InitialDistribution(gamesters, shoes, condition);
                try
                {
                    Game.ShowTable(dealer.Hands[0], gamesters);
                }
                catch (Exception)
                {
                    Assert.Fail();
                }
                dealer.GetCardsBack(gamesters);
            }
                
            Assert.Pass();
        }
        

    }
}