using NUnit.Framework;
using BlackjackMechanics.Players;

namespace BlackackTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Player n = new Player(1000);
            Assert.IsTrue(n.Money == 1000);
        }
    }
}