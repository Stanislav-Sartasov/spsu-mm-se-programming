using NUnit.Framework;

namespace ToolKit.UnitTests
{
    public class ShoeTests
    {
        [Test]
        public void CreateShoeTest()
        {
            Shoe shoe = new Shoe();
            Assert.AreEqual(shoe.NumberOfDecks, 8);
            shoe = new Shoe(0);
            Assert.AreEqual(shoe.NumberOfDecks, 8);
            shoe = new Shoe(9);
            Assert.AreEqual(shoe.NumberOfDecks, 8);

            for (int i = 1; i < 9; i++)
            {
                shoe = new Shoe(i);
                Assert.AreEqual(shoe.NumberOfDecks, i);
            }
        }

        [Test]
        public void CheckUpdateTest()
        {
            Shoe shoe = new Shoe();
            Assert.AreEqual(shoe.CheckUpdate(), false);
            shoe.TakeCards(300);
            Assert.AreEqual(shoe.CheckUpdate(), true);
        }
    }
}