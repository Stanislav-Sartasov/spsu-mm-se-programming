using NUnit.Framework;
using DecksLibrary;

namespace BlackjackLibrary.UnitTests
{
    public class BlackjackLibraryTests
    {
        [Test]
        public void DealersTurnTest()
        {
            Decks playingDecks = new Decks();
            playingDecks.FillCards();
            byte[] gameCards = new byte[416];
            byte[] dealerCards = new byte[21];
            byte testResult;
            uint playerScore;
            for (int i = 0; i < 21; i++)
            {
                dealerCards[i] = 0;
            }
            gameCards = playingDecks.Cards;
            gameCards[0] = 5; gameCards[1] = 6; gameCards[2] = 7; playerScore = 10;
            dealerCards[0] = 5; dealerCards[1] = 6; dealerCards[2] = 7; testResult = 1;
            SomeDealersTurnTest(playingDecks, playerScore, dealerCards, testResult);

            playerScore = 18; testResult = 2; gameCards[0] = 5; gameCards[1] = 6; gameCards[2] = 7;
            SomeDealersTurnTest(playingDecks, playerScore, dealerCards, testResult);

            playerScore = 19; testResult = 0; gameCards[0] = 5; gameCards[1] = 6; gameCards[2] = 7;
            SomeDealersTurnTest(playingDecks, playerScore, dealerCards, testResult);

            gameCards[0] = 10; gameCards[1] = 1; playerScore = 21;
            dealerCards[0] = 10; dealerCards[1] = 1; dealerCards[2] = 0; testResult = 2;
            SomeDealersTurnTest(playingDecks, playerScore, dealerCards, testResult, 1);

            gameCards[0] = 5; gameCards[1] = 5; gameCards[2] = 1; playerScore = 21;
            dealerCards[0] = 5; dealerCards[1] = 5; dealerCards[2] = 1; testResult = 2;
            SomeDealersTurnTest(playingDecks, playerScore, dealerCards, testResult);

            gameCards[0] = 8; gameCards[1] = 1; gameCards[2] = 0; playerScore = 17;
            dealerCards[0] = 8; dealerCards[1] = 1; dealerCards[2] = 0; testResult = 1;
            SomeDealersTurnTest(playingDecks, playerScore, dealerCards, testResult);
        }

        public void SomeDealersTurnTest(Decks playingDecks, uint playerScore, byte[] dealerCards, byte testResult, byte bjFlag = 0)
        {
            Dealer testingDealer = new Dealer(playingDecks, bjFlag);
            byte result = testingDealer.DealersTurn(playerScore);
            Assert.AreEqual(result, testResult);
            for (int i = 0; i < 21; i++)
            {
                Assert.AreEqual(testingDealer.DealerCards[i], dealerCards[i]);
            }
        }

        [Test]
        public void GameTest()
        {
            Decks playingDecks = new Decks();
            playingDecks.FillCards();
            byte[] gameCards = new byte[416];
            gameCards = playingDecks.Cards;
            byte bjFlag;
            int testResult;
            // Dealer has blackjack and player in both hands has blackjack too
            gameCards[0] = 10; gameCards[1] = 1; gameCards[2] = 1; gameCards[3] = 10;
            gameCards[4] = 10; gameCards[5] = 1; bjFlag = 3; testResult = 32;
            SomeGameTest(playingDecks, gameCards, testResult, bjFlag);

            gameCards[0] = 10; gameCards[1] = 10; gameCards[2] = 10; gameCards[3] = 1;
            bjFlag = 0; testResult = -16; // Dealer has blackjack, player's score is 20.
            SomeGameTest(playingDecks, gameCards, testResult, bjFlag);

            gameCards[0] = 10; gameCards[1] = 10; gameCards[2] = 10; gameCards[3] = 7;
            bjFlag = 0; testResult = 32; // Dealer has 17, player's score is 20.
            SomeGameTest(playingDecks, gameCards, testResult, bjFlag);

            gameCards[0] = 10; gameCards[1] = 10; gameCards[2] = 5; gameCards[3] = 7;
            bjFlag = 0; testResult = 8; // Player surrendered
            SomeGameTest(playingDecks, gameCards, testResult, bjFlag);

            gameCards[0] = 9; gameCards[1] = 8; gameCards[2] = 8; gameCards[3] = 9; gameCards[4] = 10;
            gameCards[5] = 6; gameCards[6] = 10; bjFlag = 0; testResult = 64; // Player won with both hands
            SomeGameTest(playingDecks, gameCards, testResult, bjFlag);

            // With cards counter strategy player doubles and wins, dealer has 5, 10, 10
            playingDecks.FillCards();
            gameCards = playingDecks.Cards;
            Bots strategy = Bots.CardsCounterStrategy;
            gameCards[0] = 5; gameCards[1] = 3; gameCards[2] = 5; gameCards[3] = 10; gameCards[4] = 10;
            gameCards[5] = 10; bjFlag = 0; testResult = 64;
            SomeGameTest(playingDecks, gameCards, testResult, bjFlag, strategy);
            // But with basic strategy he just hits
            strategy = Bots.BasicStrategy;
            gameCards[0] = 5; gameCards[1] = 3; gameCards[2] = 5; gameCards[3] = 10; gameCards[4] = 10;
            gameCards[5] = 10; bjFlag = 0; testResult = 32;
            SomeGameTest(playingDecks, gameCards, testResult, bjFlag, strategy);
            // With simple strategy player doubles and wins, dealer has 10, 6, 10
            strategy = Bots.SimpleStrategy;
            gameCards[0] = 10; gameCards[1] = 5; gameCards[2] = 5; gameCards[3] = 10; gameCards[4] = 6;
            gameCards[5] = 10; bjFlag = 0; testResult = 64;
            SomeGameTest(playingDecks, gameCards, testResult, bjFlag, strategy);
            // But with basic strategy he just hits, dealer has 10, 6, 10
            strategy = Bots.BasicStrategy;
            gameCards[0] = 10; gameCards[1] = 5; gameCards[2] = 5; gameCards[3] = 10; gameCards[4] = 6;
            gameCards[5] = 10; bjFlag = 0; testResult = 32;
            SomeGameTest(playingDecks, gameCards, testResult, bjFlag, strategy);
            // Now we test two games successively
            playingDecks.FillCards();
            gameCards = playingDecks.Cards;
            gameCards[0] = 10; gameCards[1] = 10; gameCards[2] = 5; gameCards[3] = 7;
            bjFlag = 0; testResult = 8; // Player surrendered
            uint initialMoney = 1600;
            BlackjackGame testGame = new BlackjackGame(playingDecks, initialMoney, strategy);
            Assert.AreEqual(testGame.Game(), testResult);
            for (int i = 0; i < 416; i++)
            {
                Assert.AreEqual(testGame.PlayingCards.Cards[i], gameCards[i]);
            }
            // Second game
            gameCards[0] = 9; gameCards[1] = 8; gameCards[2] = 8; gameCards[3] = 9; gameCards[4] = 10;
            gameCards[5] = 6; gameCards[6] = 10; bjFlag = 0; testResult = 64; // Player won with both hands
            Assert.AreEqual(testGame.Game(), testResult);
            for (int i = 0; i < 416; i++)
            {
                Assert.AreEqual(testGame.PlayingCards.Cards[i], gameCards[i]);
            }
        }

        public void SomeGameTest(Decks playingCards, byte[] gameCards, int result, byte bjFlag, Bots strategy = Bots.BasicStrategy)
        {
            uint initialMoney = 1600;
            BlackjackGame testGame = new BlackjackGame(playingCards, initialMoney, strategy);
            Assert.AreEqual(testGame.Game(), result);
            for (int i = 0; i < 416; i++)
            {
                Assert.AreEqual(testGame.PlayingCards.Cards[i], gameCards[i]);
            }
        }
    }
}