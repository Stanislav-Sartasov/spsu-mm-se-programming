using Task_1.Collections;

namespace Task_1.Tests
{
    public class CollectionsTests
    {

        [Test]
        public void ContainsCuckooTest()
        {
            var examSystem = new StripedCuckooHashSet(10);

            examSystem.Add(10, 15);

            Assert.IsTrue(examSystem.Contains(10, 15));
        }

        [Test]
        public void RemoveCuckooTest()
        {
            var examSystem = new StripedCuckooHashSet(10);

            examSystem.Add(10, 15);
            examSystem.Add(15, 15);

            Assert.IsTrue(examSystem.Contains(10, 15));

            examSystem.Remove(10, 15);

            Assert.IsTrue(!examSystem.Contains(10, 15));
            Assert.IsTrue(examSystem.Contains(15, 15));
        }

        [Test]
        public void ContainsLazySetTest()
        {
            var examSystem = new LazySet();

            examSystem.Add(10, 15);

            Assert.IsTrue(examSystem.Contains(10, 15));
        }

        [Test]
        public void RemoveLazySetTest()
        {
            var examSystem = new LazySet();

            examSystem.Add(10, 15);
            examSystem.Add(15, 15);

            Assert.AreEqual(examSystem.Count, 2);

            Assert.IsTrue(examSystem.Contains(10, 15));

            examSystem.Remove(10, 15);

            Assert.IsTrue(!examSystem.Contains(10, 15));
            Assert.IsTrue(examSystem.Contains(15, 15));
        }
    }
}