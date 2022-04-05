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
            uint initialMoney = 10;
            Dealer testingDealer = new Dealer(playingDecks, initialMoney, 0, bjFlag);
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
            byte[] dealerCards = new byte[21];
            byte bjFlag;
            int testResult;
            for (int i = 0; i < 21; i++)
            {
                dealerCards[i] = 0;
            }
            gameCards[0] = 10; gameCards[1] = 1; gameCards[2] = 1; gameCards[3] = 10; gameCards[4] = 10; gameCards[5] = 1;
            dealerCards[0] = 10; dealerCards[1] = 1; // Dealer has blackjack and player in both hands has blackjack too
            bjFlag = 3; testResult = 32;
            SomeGameTest(playingDecks, dealerCards, gameCards, testResult, bjFlag);

            gameCards[0] = 10; gameCards[1] = 10; gameCards[2] = 10; gameCards[3] = 1;
            dealerCards[0] = 10; dealerCards[1] = 1; bjFlag = 0; testResult = -16; // Dealer has blackjack, player's score is 20.
            SomeGameTest(playingDecks, dealerCards, gameCards, testResult, bjFlag);

            gameCards[0] = 10; gameCards[1] = 10; gameCards[2] = 10; gameCards[3] = 7;
            dealerCards[0] = 10; dealerCards[1] = 7; bjFlag = 0; testResult = 32; // Dealer has 17, player's score is 20.
            SomeGameTest(playingDecks, dealerCards, gameCards, testResult, bjFlag);

            gameCards[0] = 10; gameCards[1] = 10; gameCards[2] = 5; gameCards[3] = 7;
            dealerCards[0] = 10; dealerCards[1] = 0; bjFlag = 0; testResult = 8; // Player surrendered
            SomeGameTest(playingDecks, dealerCards, gameCards, testResult, bjFlag);

            gameCards[0] = 9; gameCards[1] = 8; gameCards[2] = 8; gameCards[3] = 9; gameCards[4] = 10;
            gameCards[5] = 6; gameCards[6] = 10; dealerCards[0] = 9; dealerCards[1] = 6; dealerCards[2] = 10;
            bjFlag = 0; testResult = 64; // Player won with both hands
            SomeGameTest(playingDecks, dealerCards, gameCards, testResult, bjFlag);

            // With cards counter strategy player doubles and wins
            playingDecks.FillCards();
            gameCards = playingDecks.Cards;
            Bots strategy = Bots.CardsCounterStrategy;
            gameCards[0] = 5; gameCards[1] = 3; gameCards[2] = 5; gameCards[3] = 10; gameCards[4] = 10;
            gameCards[5] = 10; dealerCards[0] = 5; dealerCards[1] = 10; dealerCards[2] = 10; bjFlag = 0; testResult = 64;
            SomeGameTest(playingDecks, dealerCards, gameCards, testResult, bjFlag, strategy);
            // But with basic strategy he just hits
            strategy = Bots.BasicStrategy;
            gameCards[0] = 5; gameCards[1] = 3; gameCards[2] = 5; gameCards[3] = 10; gameCards[4] = 10;
            gameCards[5] = 10; dealerCards[0] = 5; dealerCards[1] = 10; dealerCards[2] = 10; bjFlag = 0; testResult = 32;
            SomeGameTest(playingDecks, dealerCards, gameCards, testResult, bjFlag, strategy);
            // With simple strategy player doubles and wins
            strategy = Bots.SimpleStrategy;
            gameCards[0] = 10; gameCards[1] = 5; gameCards[2] = 5; gameCards[3] = 10; gameCards[4] = 6;
            gameCards[5] = 10; dealerCards[0] = 10; dealerCards[1] = 6; dealerCards[2] = 10; bjFlag = 0; testResult = 64;
            SomeGameTest(playingDecks, dealerCards, gameCards, testResult, bjFlag, strategy);
            // But with basic strategy he just hits
            strategy = Bots.BasicStrategy;
            gameCards[0] = 10; gameCards[1] = 5; gameCards[2] = 5; gameCards[3] = 10; gameCards[4] = 6;
            gameCards[5] = 10; dealerCards[0] = 10; dealerCards[1] = 6; dealerCards[2] = 10; bjFlag = 0; testResult = 32;
            SomeGameTest(playingDecks, dealerCards, gameCards, testResult, bjFlag, strategy);
        }

        public void SomeGameTest(Decks playingCards, byte[] dealerCards, byte[] gameCards, int result, byte bjFlag, Bots strategy = Bots.BasicStrategy)
        {
            uint initialMoney = 1600;
            Dealer testDealer = new Dealer(playingCards, initialMoney, strategy);
            Assert.AreEqual(testDealer.Game(), result);
            for (int i = 0; i < 21; i++)
            {
                Assert.AreEqual(testDealer.DealerCardsCopy[i], dealerCards[i]);
            }
            for (int i = 0; i < 416; i++)
            {
                Assert.AreEqual(testDealer.PlayingDecks.Cards[i], gameCards[i]);
            }
        }
    }
}