namespace PCLib
{
	public class Consumer
	{
		private ILock locker;
		private List<string> buffer;

		private Thread thread;
		private volatile bool stop;

		private Guid guid;

		public Consumer(ILock locker, List<string> buffer)
		{
			this.locker = locker;
			this.buffer = buffer;

			thread = new Thread(Consume);
			stop = false;

			guid = Guid.NewGuid();
		}

		public void Start()
		{
			thread.Start();
		}

		private void Consume()
		{
			while (!stop)
			{
				locker.Lock();
				if (buffer.Count > 0)
				{
					var data = buffer.First();
					buffer.RemoveAt(0);
					Console.WriteLine($"Consumer {guid} received {data}");
				}
				locker.Unlock();

				Thread.Sleep(1000);
			}
		}

		public void Join()
		{
			Stop();
			thread.Join();
		}

		private void Stop()
		{
			stop = true;
		}
	}
}
