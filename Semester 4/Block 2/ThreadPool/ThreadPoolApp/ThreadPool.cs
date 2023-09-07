namespace ThreadPoolApp
{
	public class ThreadPool : IDisposable
	{
		private const int poolSize = 4;

		private readonly List<Thread> pool = new List<Thread>();
		private readonly Queue<Action> queue = new Queue<Action>();

		private readonly object sync = new object();
		private volatile bool stop = false;

		public ThreadPool()
		{
			for (var i = 0; i < poolSize; i++)
			{
				var thread = new Thread(Run);
				thread.Name = Guid.NewGuid().ToString();
				pool.Add(thread);
				thread.Start();
			}
		}

		private void Run()
		{
			while (true)
			{
				Action task;

				Monitor.Enter(sync);
				try
				{
					while (queue.Count == 0 && !stop)
						Monitor.Wait(sync);

					if (stop) return;

					task = queue.Dequeue();
				}
				finally
				{
					Monitor.Exit(sync);
				}

				task.Invoke();
			}
		}

		public void Enqueue(Action task)
		{
			if (stop) throw new ObjectDisposedException("Thread pool is disposed");

			Monitor.Enter(sync);
			try
			{
				queue.Enqueue(task);
				Monitor.PulseAll(sync);
			}
			finally
			{
				Monitor.Exit(sync);
			}
		}

		public void Dispose()
		{
			stop = true;

			Monitor.Enter(sync);
			try
			{
				queue.Clear();
				Monitor.PulseAll(sync);
			}
			finally
			{
				Monitor.Exit(sync);
			}

			foreach (var thread in pool)
				thread.Join();
		}
	}
}
