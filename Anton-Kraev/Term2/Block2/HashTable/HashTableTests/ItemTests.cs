using HashTable;
using NUnit.Framework;

namespace HashTableTests
{
    public class ItemTests
    {
        private Item<string> x;
        private Item<string> y;

        [SetUp]
        public void SetUp()
        {
            x = new Item<string>("abc");
            x.Next = new Item<string>("def");
            y = (Item<string>)x.Clone();
        }

        [Test]
        public void CloneAccuracyTest()
        {
            Assert.AreEqual(x.Data, y.Data);
            Assert.AreEqual(x.Next.Data, y.Next.Data);
        }

        [Test]
        public void DeepCopyTest()
        {
            y.Data = "third";
            x.Data = "first";
            x.Next.Data = "second";
            y.Next.Data = "fourth";

            Assert.AreEqual("first", x.Data);
            Assert.AreEqual("second", x.Next.Data);
            Assert.AreEqual("third", y.Data);
            Assert.AreEqual("fourth", y.Next.Data);
        }
    }
}