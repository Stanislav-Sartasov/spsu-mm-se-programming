using NUnit.Framework;

namespace ProducerConsumer.UnitTests
{
    public class ProducerConsumerTests
    {
        [Test]
        public void ConsumerTest()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Semaphore producerSemaphore = new Semaphore(1, 1);
            Semaphore consumerSemaphore = new Semaphore(0, 1);
            List<int> buffer = new List<int>() { 2 };

            Consumer consumer = new Consumer(
                consumerSemaphore, 
                producerSemaphore, 
                buffer, 
                cancellationTokenSource, 
                1
            );

            consumer.Start();

            producerSemaphore.WaitOne(0);
            consumerSemaphore.Release();
            Thread.Sleep(3000);

            consumer.Stop();

            Assert.That(buffer.Count, Is.EqualTo(0));
        }

        [Test]
        public void ProducerTest()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Semaphore producerSemaphore = new Semaphore(1, 1);
            List<int> buffer = new List<int>();
            Semaphore consumerSemaphore = new Semaphore(0, 1);

            Producer producer = new Producer(cancellationTokenSource, buffer, producerSemaphore, consumerSemaphore, 1);
            producer.Start();
            producer.Stop();

            Assert.That(1, Is.EqualTo(buffer.Count));
        }
    }
}