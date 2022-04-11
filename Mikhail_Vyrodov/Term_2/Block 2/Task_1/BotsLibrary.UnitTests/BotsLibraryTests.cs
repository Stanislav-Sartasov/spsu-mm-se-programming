using NUnit.Framework;
using DecksLibrary;

namespace BotsLibrary.UnitTests
{
    public class BotsLibraryTests
    {
        [Test]
        public void PlayersTurnTest()
        {
            Decks playingDecks = new Decks();
            playingDecks.FillCards();
            byte[] gameCards = new byte[416];
            Decisions[] playersDecisions = new Decisions[21];
            byte bjFlag = 0, decisionsCounter, dealersCard;
            bool surrenderFlag = false;
            uint balance, initialMoney, firstWager, secondWager, firstSum, secondSum, initialWager;
            for (int i = 0; i < 21; i++)
            {
                playersDecisions[i] = Decisions.Nothing;
            }
            gameCards = playingDecks.Cards;
            // Player score is 20 and he don't do anything
            gameCards[0] = 10; gameCards[1] = 10; dealersCard = 5; decisionsCounter = 0; initialMoney = 60;
            balance = 54; firstWager = 6; secondWager = 0; firstSum = 20; secondSum = 0; initialWager = 6;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance);
            // Player has a blackjack
            gameCards[0] = 10; gameCards[1] = 1; firstSum = 21; bjFlag = 1;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance, bjFlag);
            // Player doubles and ace has 1 score in this case
            gameCards[0] = 1; gameCards[1] = 8; gameCards[2] = 9; dealersCard = 6; firstSum = 18; firstWager = 12;
            balance = 48; decisionsCounter = 1; playersDecisions[0] = Decisions.Doubling;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance);
            // In this case ace is 11 score and player just hits
            gameCards[0] = 1; gameCards[1] = 7; gameCards[2] = 3; dealersCard = 9; firstSum = 21; firstWager = 6;
            balance = 54; decisionsCounter = 1; playersDecisions[0] = Decisions.Hitting; bjFlag = 0;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance, bjFlag);
            // In this case player first cards is 2 aces and he splits them and hits three times in first hand
            // and he has a blackjack in the second hand
            gameCards[0] = 1; gameCards[1] = 1; gameCards[2] = 5; gameCards[3] = 6; gameCards[4] = 10; gameCards[5] = 10;
            dealersCard = 9; playersDecisions[0] = Decisions.Splitting; playersDecisions[1] = Decisions.Hitting; // after 3rd playersDecision he plays second hand
            playersDecisions[2] = Decisions.Hitting; playersDecisions[3] = Decisions.Hitting; playersDecisions[4] = Decisions.Hitting;
            bjFlag = 2; firstSum = 0; secondSum = 21; firstWager = 6; secondWager = 6; balance = 48; decisionsCounter = 5;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance, bjFlag);

            for (int i = 0; i < 5; i++)
                playersDecisions[i] = Decisions.Nothing;
            // Player surrenders
            gameCards[0] = 10; gameCards[1] = 6; dealersCard = 9; playersDecisions[0] = Decisions.Surrendering;
            firstWager = 3; balance = 57; secondWager = 0; firstSum = 16; secondSum = 0; surrenderFlag = true;
            decisionsCounter = 1; bjFlag = 0;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance, bjFlag, surrenderFlag);

            // Now we test Cards Counter strategy
            // Instead of hitting we make doubling 
            playingDecks.FillCards();
            gameCards = playingDecks.Cards;
            bool CardsCounterStrategyFlag = true;
            gameCards[0] = 3; gameCards[1] = 5; gameCards[2] = 10; dealersCard = 9;
            firstWager = 12; balance = 48; secondWager = 0; firstSum = 18; secondSum = 0; surrenderFlag = false;
            playersDecisions[0] = Decisions.Doubling; decisionsCounter = 1; bjFlag = 0;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance, bjFlag, surrenderFlag, CardsCounterStrategyFlag);

            // Simple test of doubling function
            playingDecks.FillCards();
            gameCards = playingDecks.Cards;
            gameCards[0] = 8; gameCards[1] = 3; gameCards[2] = 9; dealersCard = 6; firstSum = 20; firstWager = 12;
            balance = 48; decisionsCounter = 1; playersDecisions[0] = Decisions.Doubling;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance, bjFlag, surrenderFlag, CardsCounterStrategyFlag);

            // Instead of hitting we make doubling because our Cards count equals 2
            playingDecks.FillCards();
            gameCards = playingDecks.Cards;
            gameCards[0] = 3; gameCards[1] = 3; gameCards[2] = 6; gameCards[3] = 10; gameCards[4] = 6; gameCards[5] = 10;
            firstSum = 19; secondSum = 19; dealersCard = 5; firstWager = 24; secondWager = 24; balance = 12; decisionsCounter = 5;
            playersDecisions[0] = Decisions.Splitting; playersDecisions[1] = Decisions.Doubling; playersDecisions[2] = Decisions.Doubling;
            playersDecisions[3] = Decisions.Doubling; playersDecisions[4] = Decisions.Doubling;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance, bjFlag, surrenderFlag, CardsCounterStrategyFlag);

            // Now we use simple strategy
            // Player initially has two fives but instead of hitting he doubles because his score is 10
            for (int i = 0; i < 5; i++)
                playersDecisions[i] = Decisions.Nothing;
            bool simpleStrategyFlag = true;
            CardsCounterStrategyFlag = false;
            playingDecks.FillCards();
            gameCards = playingDecks.Cards;
            gameCards[0] = 5; gameCards[1] = 5; gameCards[2] = 10; dealersCard = 10; firstSum = 20; firstWager = 12;
            balance = 48; decisionsCounter = 1; secondWager = 0; secondSum = 0; playersDecisions[0] = Decisions.Doubling;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance, bjFlag, surrenderFlag, CardsCounterStrategyFlag, simpleStrategyFlag);

            // Player initially has 2 aces and after his turn he has in both hands 21 score.
            playingDecks.FillCards();
            gameCards = playingDecks.Cards;
            gameCards[0] = 1; gameCards[1] = 1; gameCards[2] = 2; gameCards[3] = 8; gameCards[4] = 2; gameCards[5] = 8;
            firstSum = 21; secondSum = 21; dealersCard = 5; firstWager = 12; secondWager = 12; balance = 36; decisionsCounter = 5;
            playersDecisions[0] = Decisions.Splitting; playersDecisions[1] = Decisions.Hitting; playersDecisions[2] = Decisions.Doubling;
            playersDecisions[3] = Decisions.Hitting; playersDecisions[4] = Decisions.Doubling;
            SomePlayersTurnTest(dealersCard, initialMoney, initialWager, playingDecks, decisionsCounter, firstSum, firstWager,
                secondSum, secondWager, playersDecisions, balance, bjFlag, surrenderFlag, CardsCounterStrategyFlag, simpleStrategyFlag);
        }

        public void SomePlayersTurnTest(byte dealerCard, uint initialMoney, uint initialWager, Decks playingDecks, byte decisionsCounter, uint firstSum,
            uint firstWager, uint secondSum, uint secondWager, Decisions[] playerDecisions, uint balance, byte bjFlag = 0,
            bool surrenderFlag = false, bool cardsCounterStrategyFlag = false, bool simpleStrategyFlag = false)
        {
            if (cardsCounterStrategyFlag)
            {
                CardsCounterStrategy somePlayer = new CardsCounterStrategy(dealerCard, initialMoney, playingDecks, initialWager);
                SomeStrategyTest(somePlayer, decisionsCounter, firstSum, firstWager, secondSum, secondWager,
                    playerDecisions, balance, bjFlag, surrenderFlag);
            }
            else if (simpleStrategyFlag)
            {
                SimpleStrategy somePlayer = new SimpleStrategy(dealerCard, initialMoney, playingDecks, initialWager);
                SomeStrategyTest(somePlayer, decisionsCounter, firstSum, firstWager, secondSum, secondWager,
                    playerDecisions, balance, bjFlag, surrenderFlag);
            }
            else
            {
                Player somePlayer = new Player(dealerCard, initialMoney, playingDecks, initialWager);
                SomeStrategyTest(somePlayer, decisionsCounter, firstSum, firstWager, secondSum, secondWager,
                    playerDecisions, balance, bjFlag, surrenderFlag);
            }
        }
        public void SomeStrategyTest(Player somePlayer, byte decisionsCounter, uint firstSum,
            uint firstWager, uint secondSum, uint secondWager, Decisions[] playerDecisions, uint balance, byte bjFlag = 0,
            bool surrenderFlag = false)
        {
            somePlayer.PlayersTurn(somePlayer.FirstHand);
            for (int i = 0; i < 21; i++)
            {
                Assert.AreEqual(somePlayer.PlayersDecisions[i], playerDecisions[i]);
            }
            Assert.AreEqual(somePlayer.DecisionsCounter, decisionsCounter);
            Assert.AreEqual(somePlayer.Money, balance);
            Assert.AreEqual(somePlayer.FirstSum, firstSum);
            Assert.AreEqual(somePlayer.SecondSum, secondSum);
            Assert.AreEqual(somePlayer.FirstWager, firstWager);
            Assert.AreEqual(somePlayer.SecondWager, secondWager);
            Assert.AreEqual(somePlayer.BjFlag, bjFlag);
            Assert.AreEqual(somePlayer.SurrFlag, surrenderFlag);
        }
    }
}