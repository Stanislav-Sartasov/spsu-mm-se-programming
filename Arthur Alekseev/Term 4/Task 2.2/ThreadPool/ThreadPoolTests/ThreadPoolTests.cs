namespace ThreadPoolTests;

public class Tests
{
	[Test]
	public void ThreadPoolTest()
	{
		var counter = 0;

		void Increment()
		{
			Interlocked.Add(ref counter, 1);
		}

		using var pool = new ThreadPool.ThreadPool();

		for (var i = 0; i < 2000; i++)
			pool.Enqueue(Increment);

		Thread.Sleep(3000);

		Assert.AreEqual(counter, 2000);
	}
}