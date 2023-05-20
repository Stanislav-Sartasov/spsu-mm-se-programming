using ProducerConsumer.Consumer;
using ProducerConsumer.Lock;
using ProducerConsumer.Producer;
using ProducerConsumerTests.Logger;

namespace ProducerConsumerTests
{
	internal class ConsumerTests
	{
		[Test]
		public void ConsumerTest()
		{
			var logger = new StringLogger();
			var lockObject = new TASLock();
			var buffer = new List<float>() {1};
			var consumer = new RandomFloatConsumer(lockObject, buffer, logger);

			consumer.Start();
			Thread.Sleep(1100);
			consumer.Stop();
			consumer.Wait();

			Assert.IsTrue(buffer.Count != 1);

			var log = logger.FullLog;

			Assert.IsTrue(log[0].StartsWith("Starting consumer"));
			Assert.IsTrue(log[1].StartsWith("Consumed"));
			Assert.IsTrue(log[^2].StartsWith("Stopping consumer"));
			Assert.IsTrue(log[^1].StartsWith("Stopped consumer"));
		}
	}
}