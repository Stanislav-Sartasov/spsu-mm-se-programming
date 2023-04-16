using ProducerConsumer.LoopingThread;

namespace ProducerConsumerTests
{
	public class Tests
	{
		private static int LoopAmnt;

		[Test]
		public void LoopThreadCreate()
		{
			LoopAmnt = 0;
			Assert.DoesNotThrow(() =>
			{
				var t = new LoopingThread(ThreadIteration);
			});

			Assert.AreEqual(LoopAmnt, 0);
		}

		[Test]
		public void LoopThreadRun()
		{
			LoopAmnt = 0;
			var t = new LoopingThread(ThreadIteration);
			t.Start();
			Thread.Sleep(50);
			t.Stop();
			t.Wait();
			Assert.IsTrue(LoopAmnt != 0);
		}

		private static void ThreadIteration()
		{
			LoopAmnt++;
			Thread.Sleep(10);
		}
	}
}