namespace ThreadPoolTests
{
	public class Tests
	{
		[Test]
		public void ActionExecutionTest()
		{
			var actionNumber = 10;
			var flags = new List<int>();
			for (int i = 0; i < actionNumber; i++)
				flags.Add(0);

			using (var pool = new ThreadPool.ThreadPool())
			{
				for (int i = 0; i < actionNumber; i++)
				{
					var personalI = i;
					pool.Enqueue(() =>
					{
						flags[personalI]++;
					});
				}

				Thread.Sleep(1000);
			}

			for (int i = 0; i < actionNumber; i++)
				Assert.That(flags[i], Is.EqualTo(1));
		}

		[Test]
		public void DisposedEnqueueTest()
		{
			var pool = new ThreadPool.ThreadPool();
			pool.Dispose();
			Assert.Throws<ObjectDisposedException>(() => pool.Enqueue(() => { }));
		}
	}
}