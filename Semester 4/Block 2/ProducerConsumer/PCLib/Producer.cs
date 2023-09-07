namespace PCLib
{
	public class Producer
	{
		private ILock locker;
		private List<string> buffer;

		private Thread thread;
		private volatile bool stop;

		private Guid guid;

		public Producer(ILock locker, List<string> buffer)
		{
			this.locker = locker;
			this.buffer = buffer;

			thread = new Thread(Produce);
			stop = false;

			guid = Guid.NewGuid();
		}

		public void Start()
		{
			thread.Start();
		}

		private void Produce()
		{
			while (!stop)
			{
				var data = GenerateData();

				locker.Lock();
				buffer.Add(data);
				Console.WriteLine($"Producer {guid} send {data}");
				locker.Unlock();

				Thread.Sleep(1000);
			}
		}

		private string GenerateData()
		{
			return "SOME DATA";
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
