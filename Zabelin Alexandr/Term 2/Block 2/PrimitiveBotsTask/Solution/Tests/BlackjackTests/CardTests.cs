using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blackjack;

namespace BlackjackTests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void CardTest()
        {
            uint expectedNumber = 1234;
            byte expectedWeight = 8;

            Card card = new Card(expectedNumber, expectedWeight);

            Assert.AreEqual(expectedNumber, card.Number);
            Assert.AreEqual(expectedWeight, card.Weight);
        }
    }
}