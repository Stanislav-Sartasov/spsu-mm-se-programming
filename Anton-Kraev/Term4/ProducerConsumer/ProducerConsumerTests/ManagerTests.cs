namespace ProducerConsumerTests
{
    public class ManagerTests
    {
        private static volatile int producersCounter;
        private static volatile int consumersCounter;
        private static volatile int acc;

        private static int Produce()
        {
            if (producersCounter == 10) return 0;
            Interlocked.Increment(ref producersCounter);
            return 3;
        }

        private static void Consume(int item)
        {
            if (consumersCounter == 10 || item == 0) return;
            Interlocked.Increment(ref consumersCounter);
            Interlocked.Add(ref acc, item);
        }

        private Manager<int> manager;

        [SetUp]
        public void Setup()
        {
            producersCounter = 0;
            consumersCounter = 0;
            acc = 0;

            manager = new Manager<int>(2, 2, Produce, Consume);
        }

        [Test]
        public void ManagerTest()
        {
            manager.Start();
            while (consumersCounter + producersCounter != 20) { }
            manager.Dispose();

            Assert.AreEqual(30, acc);
        }

        [Test]
        public void DoubleStartAndDisposeTest()
        {
            manager.Start();
            manager.Start();
            manager.Dispose();
            manager.Dispose();

            Assert.Pass();
        }

        [Test]
        public void StartAfterDisposeTest()
        {
            manager.Start();
            manager.Dispose();

            Assert.Catch<InvalidOperationException>(() => manager.Start());
        }
    }
}
