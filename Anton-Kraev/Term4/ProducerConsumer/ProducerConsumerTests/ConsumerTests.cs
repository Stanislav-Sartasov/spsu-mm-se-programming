using System.Reflection;

namespace ProducerConsumerTests
{
    public class ConsumerTests
    {
        private Buffer<int> buffer;
        private Mutex mutex;

        private static int acc;
        private static void Consume(int item)
        {
            acc *= item;
            Thread.Sleep(50);
        }

        private Consumer<int> consumer;

        [SetUp]
        public void Setup()
        {
            acc = 1;
            buffer = new Buffer<int>();
            mutex = new Mutex();
            consumer = new Consumer<int>(mutex, buffer, Consume);
        }

        [Test]
        public void ConsumeItemsTest()
        {
            buffer.Push(2);
            buffer.Push(3);
            buffer.Push(4);

            var thread = new Thread(consumer.Run);
            thread.Start();
            while (!buffer.IsEmpty) { }
            consumer.Stop();
            thread.Join();

            Assert.AreEqual(24, acc);
            Assert.IsTrue(buffer.IsEmpty);
        }
    }
}
