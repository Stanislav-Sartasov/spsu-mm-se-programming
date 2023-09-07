using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCLib;

namespace PCTests
{
	[TestClass]
	public class PCLibTests
	{
		[TestMethod]
		public void ProducerTest()
		{
			var locker = new TASLock();
			var buffer = new List<string>();

			var producer = new Producer(locker, buffer);
			
			producer.Start();
			Thread.Sleep(1500);
			producer.Join();

			Assert.IsTrue(buffer.Count > 1);
		}

		[TestMethod]
		public void ConsumerTest()
		{
			var locker = new TASLock();
			var buffer = new List<string>();
			buffer.Add("SOME DATA");

			var consumer = new Consumer(locker, buffer);

			consumer.Start();
			Thread.Sleep(500);
			consumer.Join();

			Assert.IsTrue(buffer.Count == 0);
		}
	}
}
