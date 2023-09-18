using ProducersConsumers;
using ProducersConsumers.Interfaces;
using Moq;

namespace ProducersConsumersTests
{
	public class ThreadedAccessTests
	{
		[Test]
		public void LockUsageTest()
		{
			var mockedLocker = new Mock<ILock>();
			var list = new List<int>();

			var producer = new Producer(mockedLocker.Object, list);
			producer.Start();
			Thread.Sleep(1500);
			producer.Stop();

			var added = list.Count;
			mockedLocker.Verify(mockedLocker => mockedLocker.Lock(), Times.Exactly(added));
			mockedLocker.Verify(mockedLocker => mockedLocker.Unlock(), Times.Exactly(added));
		}

		[Test]
		public void ConsumerTest()
		{
			var locker = new AtomLock();
			var list = new List<int> { 5 };

			var consumer = new Consumer(locker, list);
			consumer.Start();
			Thread.Sleep(150);
			consumer.Stop();

			Assert.That(list.Count, Is.EqualTo(0));
		}

		[Test]
		public void ProducerTest()
		{
			var locker = new AtomLock();
			var list = new List<int>();

			var consumer = new Producer(locker, list);
			consumer.Start();
			Thread.Sleep(150);
			consumer.Stop();

			Assert.IsTrue(list.Count > 0);
		}
	}
}