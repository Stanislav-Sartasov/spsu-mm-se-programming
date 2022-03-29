using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Cards;

namespace CardsLibraryTests
{
    public class ShoesTests
    {
        [Test]
        public void GetCardTest()
        {
            var shoes = new Shoes();
            var cardList = new List<Card>();
            for (int i = 0; i < 416; i++)
                cardList.Add(shoes.GetCard());

            Assert.AreEqual(cardList.Count, 416);
            Assert.AreEqual(cardList.Count(c => c.Name.Equals("A")), 32);
            Assert.AreEqual(shoes.AllCardsCount, 0);
        }
    }
}