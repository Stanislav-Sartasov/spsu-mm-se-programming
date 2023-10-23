namespace ProducersConsumers.Interfaces
{
	public abstract class AThreadedListAccessor
	{
		private volatile bool _isStopped = true;
		private Random _rnd = new Random();
		private Thread _thread;
		private ILock _locker;
		private static int AccessCount = 1;

		protected List<int> data;
		protected int id;

		public AThreadedListAccessor(ILock locker, List<int> list)
		{
			_locker = locker;
			data = list;
			_thread = new Thread(MainLoop);
			id = AccessCount;
			AccessCount++;
		}

		private void MainLoop()
		{
			while (!_isStopped)
			{
				_locker.Lock();
				Work();
				_locker.Unlock();
				Thread.Sleep(_rnd.Next(100, 1000));
			}
		}

		public void Start()
		{
			_isStopped = false;
			_thread.Start();
		}

		public void Stop()
		{
			_isStopped = true;
			_thread.Join();
		}

		protected abstract void Work();
	}
}
