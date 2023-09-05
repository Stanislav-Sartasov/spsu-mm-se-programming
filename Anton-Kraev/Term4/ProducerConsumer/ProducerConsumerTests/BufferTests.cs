namespace ProducerConsumerTests
{
    public class BufferTests
    {
        private Buffer<string> buffer;

        [SetUp]
        public void Setup()
        {
            buffer = new Buffer<string>();
        }

        [Test]
        public void IsEmptyTest()
        {
            Assert.IsTrue(buffer.IsEmpty);
            buffer.Push("hello");
            Assert.IsFalse(buffer.IsEmpty);
            buffer.Pop();
            Assert.IsTrue(buffer.IsEmpty);
        }

        [Test]
        public void PushPopTest()
        {
            buffer.Push("first");
            buffer.Push("second");
            var first = buffer.Pop();
            Assert.AreEqual("first", first);
            buffer.Push("third");
            var second = buffer.Pop();
            var third = buffer.Pop();
            Assert.AreEqual("second", second);
            Assert.AreEqual("third", third);
        }
    }
}