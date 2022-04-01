using System.Collections.Generic;
using BlackjackMechanics.Cards;
using AbstractClasses;
using NUnit.Framework;

namespace BlackackTests.CardsTests
{
    public class CardTests
    {
        [Test]
        public void NumberCardTest()
        {
            for (int indexSuit = 0; indexSuit < 4; indexSuit++)
            {
                for (int indexName = 0; indexName < 9; indexName++)
                {
                    CardNames exampleName = (CardNames)indexName;
                    CardSuits exampleSuit = (CardSuits)indexSuit;
                    var card = new NumberCard(exampleName, exampleSuit);

                    Assert.IsTrue(card.CardSuit == exampleSuit && card.CardName == exampleName && card.CardNumber == indexName + 2);
                }
            }
        }

        [Test]
        public void FaceCardTest()
        {
            for (int indexSuit = 0; indexSuit < 4; indexSuit++)
            {
                for (int indexName = 9; indexName < 12; indexName++)
                {
                    CardNames exampleName = (CardNames)indexName;
                    CardSuits exampleSuit = (CardSuits)indexSuit;
                    var card = new FaceCard(exampleName, exampleSuit);

                    Assert.IsTrue(card.CardSuit == exampleSuit && card.CardName == exampleName && card.CardNumber == 10);
                }
            }
        }

        [Test]
        public void AceCardTest()
        {
            for (int indexSuit = 0; indexSuit < 4; indexSuit++)
            {
                CardSuits exampleSuit = (CardSuits)indexSuit;
                var card = new AceCard(exampleSuit);

                Assert.IsTrue(card.CardSuit == exampleSuit && card.CardName == CardNames.Ace && card.CardNumber == 11);
            }
        }

        [Test]
        public void AceCardNumberChangingTest()
        {
            var listOfCards = new List<ACard>();
            listOfCards.Add(new FaceCard(CardNames.Jack, CardSuits.Diamond));
            listOfCards.Add(new FaceCard(CardNames.Jack, CardSuits.Diamond));
            var card = new AceCard(CardSuits.Heart);
            listOfCards.Add(card);
            card.CheckIsMoreThenTwentyOne(listOfCards);

            Assert.AreEqual(card.CardNumber, 1);
        }
    }
}
