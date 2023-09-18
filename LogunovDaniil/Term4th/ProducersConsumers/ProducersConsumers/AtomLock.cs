using ProducersConsumers.Interfaces;

namespace ProducersConsumers
{
	public class AtomLock : ILock
	{
		private volatile int _var = 0;

		public void Lock()
		{
			while (Interlocked.CompareExchange(ref _var, 1, 0) == 1);
		}

		public void Unlock()
		{
			_var = 0;
		}
	}
}
