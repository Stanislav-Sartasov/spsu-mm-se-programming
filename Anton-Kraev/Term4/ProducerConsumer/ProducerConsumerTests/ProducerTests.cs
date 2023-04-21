using System.Reflection;

namespace ProducerConsumerTests
{
    public class ProducerTests
    {
        private Buffer<int> buffer;
        private Mutex mutex;

        private static int invokeCounter;
        private static int Produce()
        {
            invokeCounter++;
            Thread.Sleep(50);
            return 123;
        }

        private Producer<int> producer;

        [SetUp]
        public void Setup()
        {
            invokeCounter = 0;
            buffer = new Buffer<int>();
            mutex = new Mutex();
            producer = new Producer<int>(mutex, buffer, Produce);
        }

        [Test]
        public void ProduceItemsTest()
        {
            var thread = new Thread(producer.Run);
            thread.Start();
            while (invokeCounter != 2) { }
            producer.Stop();
            thread.Join();

            var bufferStore = (List<int>) typeof(Buffer<int>)
                .GetField("store", BindingFlags.NonPublic | BindingFlags.Instance)?
                .GetValue(buffer)!;

            Assert.AreEqual(2, invokeCounter);
            Assert.AreEqual(invokeCounter, bufferStore.Count);
            Assert.AreEqual(246, buffer.Pop() + buffer.Pop());
        }
    }
}
