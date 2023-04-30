using NUnit.Framework;
using Dekanat.DekanatLib.StripedHashSet;

namespace DekanatUnitTests
{
    public class StripedHashSetUnitTests
    {
        [Test]
        public void AddTest()
        {
            var sys = new StripedHashSet(4);
            sys.Add(1, 1);

            Assert.AreEqual(1, sys.Count());

            sys.Add(1, 1);

            Assert.AreEqual(1, sys.Count());
        }

        [Test]
        public void RemoveTest()
        {
            var sys = new StripedHashSet(4);
            sys.Add(1, 1);
            sys.Remove(1, 1);

            Assert.AreEqual(0, sys.Count());

            sys.Add(1, 1);
            sys.Remove(2, 2);

            Assert.AreEqual(1, sys.Count());
        }

        [Test]
        public void ContainsTest()
        {
            var sys = new StripedHashSet(4);
            sys.Add(1, 1);

            Assert.IsTrue(sys.Contains(1, 1));
            Assert.IsTrue(!sys.Contains(2, 2));
        }

        [Test]
        public void CountTest()
        {
            var sys = new StripedHashSet(4);
            sys.Add(1, 1);
            sys.Add(2, 1);
            sys.Add(3, 1);
            sys.Add(4, 1);

            Assert.AreEqual(4, sys.Count());
        }

        [Test]
        public void ResizeTest()
        {
            var sys = new StripedHashSet(2);

            for(var i = 0 ; i < 16; i++)
                sys.Add(i, 1);

            Assert.Pass();
        }
    }
}