using NUnit.Framework;

namespace BlackJack.UnitTests
{
    public class ShuffleMachineTests
    {
        [Test]
        public void TrowCardTest()
        {
            ShuffleMachine machine = new ShuffleMachine();
            Assert.IsTrue(machine.TrowCard() == new Card("H", "2"));
            Assert.IsTrue(machine.TrowCard() == new Card("D", "3"));

            Assert.Pass();
        }

        [Test]
        public void ShuffleTest()
        {
            ShuffleMachine machine = new ShuffleMachine();
            machine.Shuffle();
            Assert.IsFalse(machine.TrowCard() == new Card("H", "2"));
            Assert.IsFalse(machine.TrowCard() == new Card("D", "3"));

            Assert.Pass();
        }
    }
}
