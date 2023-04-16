using ProducerConsumer.Lock;
using ProducerConsumer.Producer;
using ProducerConsumerTests.Logger;

namespace ProducerConsumerTests
{
	internal class ProducerTests
	{
		[Test]
		public void ProducerTest()
		{
			var logger = new StringLogger();
			var lockObject = new TASLock();
			var buffer = new List<float>();
			var producer = new RandomFloatProducer(lockObject, buffer, logger);

			producer.Start();
			Thread.Sleep(1100);
			producer.Stop();
			producer.Wait();

			Assert.IsTrue(buffer.Count > 1);

			var log = logger.FullLog;

			Assert.IsTrue(log[0].StartsWith("Starting producer"));
			Assert.IsTrue(log[1].StartsWith("Produced"));
			Assert.IsTrue(log[^2].StartsWith("Stopping producer"));
			Assert.IsTrue(log[^1].StartsWith("Stopped producer"));
		}
	}
}
