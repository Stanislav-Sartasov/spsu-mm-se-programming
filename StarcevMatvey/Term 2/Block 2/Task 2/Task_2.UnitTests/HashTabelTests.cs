using NUnit.Framework;
using LibraryGeneric;

namespace Task_2.UnitTests
{
    public class HashTabelTests
    {
        [Test]
        public void GetTest()
        {
            HashTabel<int>  hashTabel = new HashTabel<int>();
            hashTabel.Add(1, 2);
            Assert.AreEqual(hashTabel.Get(1), 2);
            Assert.AreEqual(hashTabel.Get(2), default(int));
            Assert.Pass();
        }

        [Test]
        public void AddTest()
        {
            HashTabel<int> hashTabel = new HashTabel<int>();
            hashTabel.Add(1, 2);
            Assert.AreEqual(hashTabel.Size, 1);
            Assert.Pass();
        }

        [Test]
        public void RebalanceTest()
        {
            HashTabel<int> hashTabel = new HashTabel<int>();
            hashTabel.Add(1, 2);
            hashTabel.Add(3, 3);
            Assert.AreEqual(hashTabel.Divisior, 4);
            Assert.Pass();
        }

        [Test]
        public void RemoveTest()
        {
            HashTabel<int> hashTabel = new HashTabel<int>();
            hashTabel.Add(1, 2);
            hashTabel.Remove(1);
            Assert.AreEqual(hashTabel.Get(1), default(int));
            Assert.AreEqual(hashTabel.Size, 0);
            Assert.Pass();
        }
    }
}