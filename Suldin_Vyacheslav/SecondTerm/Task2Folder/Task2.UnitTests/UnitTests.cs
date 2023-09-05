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
            Bot testBot = new Bot(1);
            List<Gamester> gamesters = new List<Gamester> { testBot };
            Dealer dealer = new Dealer();


            for (int i = 3; i < 30; i++)
            {
                for (int j = 3; j < 30; j++)
                {
                    GiveBotMoney(testBot);
                    for (int k = 0; k < i; k++)
                    {
                        testBot.ReceiveCard(new Card(CardSuit.Clubs,CardRank.Ace),0);
                    }
                    for (int k = 0; k < j; k++)
                    {
                        dealer.ReceiveCard(new Card(CardSuit.Clubs, CardRank.Ace), 0);
                    }

                    dealer.CalculateBets(gamesters);

                    if (testBot.GetBet(0) != 0) Assert.Fail();

                    switch (testBot.GiveResponce())
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

                    testBot.Discard();
                    dealer.Discard();

                }
            }

            testBot.Discard();
            dealer.GiveBlackJack(testBot, 0);

            for (int i = 2; i < 21; i++)
            {
                for (int k = 0; k < i; k++)
                {
                    dealer.ReceiveCard(new Card(CardSuit.Clubs, CardRank.Ace), 0);
                }
                GiveBotMoney(testBot);
                dealer.CalculateBets(gamesters);
                if ((testBot.GiveResponce() != 250 || testBot.GetBet(0) != 0) && i != 11) Assert.Fail();
                dealer.Discard();
            }

            dealer.Discard();
            dealer.GiveBlackJack(dealer, 0);

            GiveBotMoney(testBot);

            dealer.CalculateBets(gamesters);
            if (testBot.GiveResponce() != 100 || testBot.GetBet(0) != 0) Assert.Fail();

            dealer.GetCardsBack(gamesters);
            dealer.GiveBlackJack(dealer, 0);

            for (int i = 2; i < 21; i++)
            {
                for (int k = 0; k < i; k++)
                {
                    testBot.ReceiveCard(new Card(CardSuit.Clubs, CardRank.Ace), 0);
                }
                GiveBotMoney(testBot);
                dealer.CalculateBets(gamesters);
                if (testBot.GiveResponce() != 0 || testBot.GetBet(0) != 0) Assert.Fail();
                testBot.Discard();
            }
            Assert.Pass();
        }

        public void GiveBotMoney(Bot testBot)
        {
            testBot.ChangeBank(-testBot.GiveResponce());
            testBot.ChangeBank(100);
            testBot.MakeBet(0);
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
                        bot.ReceiveCard(new Card(CardSuit.Clubs, (CardRank)j), 0);
                        bot.ReceiveCard(new Card(CardSuit.Clubs, (CardRank)k), 0);
                        for (int i = 1; i < 14; i++)
                            if (bot.Answer(0, new List<Card> { new Card(CardSuit.Clubs, (CardRank)i) }, gamesters) == (PlayerMove)(-1) )
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
                    Card testCard = new Card((CardSuit)j, (CardRank)i);
                    gamester.ReceiveCard(testCard, 0);
                    if (//gamester.ScanHand(0)[0].GetCardInfo()[0] != i + 1 ||
                    //gamester.ScanHand(0)[0].GetCardInfo()[1] != j + 1 ||
                    (gamester.GetSum(0) != i && i <= 10) ||
                    (gamester.GetSum(0) != 10 && i > 10) ||
                    gamester[0][0].GetCardValue() != gamester.GetSum(0))
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
                        gamester.ReceiveCard(deck[i], j);

            dealer.GetCardsBack(gamesters);
            for (int k = 0; k < gamesters.Count; k++)
                for (int j = 0; j < 4; j++)
                    if (gamesters[k][j].Count != 0)
                        Assert.Fail();

            Assert.Pass();
        }

        [Test]

        public void IsBlackJackTest()
        {
            Card[] testSet = new Card[13];

            for (int i = 0; i < 13; i++)
            {
                testSet[i] = new Card(CardSuit.Clubs, (CardRank)i);
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
                        if ((player.GetSum(0) != 11)
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
                Card testCard = new Card(CardSuit.Clubs, (CardRank)i);

                if (testCard.GetCardValue() != result)
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
                justPlayer.ChangeBank(-justPlayer.GiveResponce());
                justPlayer.ChangeBank(10000);
                justPlayer.MakeBet(j);
                if (justPlayer.GetBet(j) != 100 || justPlayer.GiveResponce() != 10000 - 100) Assert.Fail();
                for (int k = 0; k < 5; k++) 
                {
                    marty.ChangeBank(10000);
                    marty.LastBank = marty.GiveResponce() + 1;
                    marty.LoseStreak = k;
                    marty.MakeBet(j);

                    testBot.ChangeBank(10000);
                    testBot.Step = k;
                    testBot.LastBank = 0;
                    testBot.MakeBet(j);

                    oscar.Wins = k;
                    oscar.ChangeBank(10000);
                    oscar.LastBank = 10001;
                    oscar.MakeBet(j);

                    switch (k)
                    {
                        case 0:
                            {
                                if (marty.GiveResponce() != 9800 || marty.GetBet(j) != 200
                                    || testBot.GetBet(j) != 50 || testBot.GiveResponce() != 9950
                                    || oscar.GetBet(j) != 100 || oscar.GiveResponce() != 9900) Assert.Fail();
                                break;
                            }
                        case 1:
                            {
                                if (marty.GiveResponce() != 9600 || marty.GetBet(j) != 400
                                    || testBot.GiveResponce() != 9850 || testBot.GetBet(j) != 150
                                    || oscar.GetBet(j) != 103 || oscar.GiveResponce() != 9897) Assert.Fail();
                                break;
                            }
                        case 2:
                            {
                                if (marty.GiveResponce() != 9200 || marty.GetBet(j) != 800
                                    || testBot.GetBet(j) != 100 || testBot.GiveResponce() != 9900
                                    || oscar.GetBet(j) != 106 || oscar.GiveResponce() != 9894) Assert.Fail();
                                break;
                            }
                        case 3:
                            {
                                if (marty.GiveResponce() != 8400 || marty.GetBet(j) != 1600
                                    || testBot.GiveResponce() != 9700 | testBot.GetBet(j) != 300
                                    || oscar.GetBet(j) != 109 || oscar.GiveResponce() != 9891) Assert.Fail();
                                break;
                            }
                        case 4:
                            {
                                if (marty.GiveResponce() != 6800 || marty.GetBet(j) != 3200
                                    || testBot.Step != 0 || testBot.GetBet(j) != 50 || testBot.GiveResponce() != 9950
                                    || oscar.GetBet(j) != 112 || oscar.GiveResponce() != 9888) Assert.Fail();
                                break;
                            }
                        default:
                            {
                                if (marty.GiveResponce() != 3600 | marty.GetBet(j) != 6400) Assert.Fail();
                                break;

                            }
                    }
                    marty.ChangeBank(-marty.GiveResponce());
                    testBot.ChangeBank(-testBot.GiveResponce());
                    oscar.ChangeBank(-oscar.GiveResponce());

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
            PlayerMove[,] testCondition = new PlayerMove[1, 4] { { 0, 0, 0, 0 } };
            for (int j = 0; j < 3; j++)
                foreach (PlayerMove move in Enum.GetValues(typeof(PlayerMove)))
                {
                    if (move == PlayerMove.Show) continue;
                    testCondition[0, j] = move;
                    player.ChangeBank(1000);
                    player.ReceiveCard(new Card(CardSuit.Clubs, (CardRank)10), j);
                    for (int k = 0; k < 4; k++) player.SetBet(k, (int)move * 100);

                    switch (move)
                    {
                        case PlayerMove.Pass:
                            {

                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player[j].Count != 1 ||
                                     player.GetBet(j) != (int)move * 100) Assert.Fail("1");
                                break;
                            }
                        case PlayerMove.Call:
                            {
                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player[j].Count != 2 ||
                                     player.GetBet(j) != (int)move * 100) Assert.Fail("2");
                                break;
                            }
                        case PlayerMove.Double:
                            {
                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player[j].Count != 2 ||
                                     player.GetBet(j) != (int)move * 200) Assert.Fail("3");

                                break;
                            }
                        case PlayerMove.Split:
                            {
                                player.ReceiveCard(new Card(CardSuit.Diamonds, (CardRank)10), j);
                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player.GetBet(j + 1) != (int)move * 100 ||
                                    player[j][0] == new Card(CardSuit.Clubs, (CardRank)10) ||
                                    player[j+1][0] == new Card(CardSuit.Diamonds, (CardRank)10) ||
                                    player[j].Count != player[j+1].Count) Assert.Fail("4");
                                break;
                            }
                        case PlayerMove.Surrender:
                            {
                                dealer.Ask(testPlayers, shoes, testCondition);
                                if (player[j].Count != 1 ||
                                     player.GetBet(j) != 0) Assert.Fail("5");
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
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 13; k++)
                        for (int p = 0; p < 13; p++)
                        {
                            Card firstCard = new Card((CardSuit)i, (CardRank)k);
                            Card secondCard = new Card((CardSuit)j, (CardRank)p);
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
                    set[i].ChangeBank(100000);
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
            PlayerMove[,] condition = new PlayerMove[10, 4];

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
                    Game.ShowTable(dealer[0], gamesters);
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