namespace ThreadPool
{
	public class ThreadPool : IDisposable
	{
		private static readonly int MaxThreads = 10;

		private bool _isDisposed = false;
		private List<Thread> _threads = new();
		private Queue<Action> _workQueue = new();

		public ThreadPool()
		{
			for (int i = 0; i < MaxThreads; i++)
			{
				var thread = new Thread(MainLoop);
				_threads.Add(thread);
				thread.Start();
			}
		}

		public void Enqueue(Action work)
		{
			if (_isDisposed) throw new ObjectDisposedException("Enqueueing a work to disposed ThreadPool object!");
			lock (_workQueue)
			{
				_workQueue.Enqueue(work);
				Monitor.PulseAll(_workQueue);
			}
		}

		private void MainLoop()
		{
			while (true)
			{
				Action work;
				lock (_workQueue)
				{
					while (!_isDisposed && _workQueue.Count == 0)
						Monitor.Wait(_workQueue);

					if (_isDisposed)
						return;

					work = _workQueue.Dequeue();
				}
				work.Invoke();
			}
		}

		public void Dispose()
		{
			_isDisposed = true;

			lock (_workQueue)
				Monitor.PulseAll(_workQueue);

			foreach (var thread in _threads)
				thread.Join();
		}
	}
}
