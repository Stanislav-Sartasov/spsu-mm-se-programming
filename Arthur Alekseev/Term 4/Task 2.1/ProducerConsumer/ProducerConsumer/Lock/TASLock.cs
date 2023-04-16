using ProducerConsumer.Interfaces;

namespace ProducerConsumer.Lock;

public class TASLock : ILock
{
	private volatile int _locked;

	public void Lock()
	{
		while (Interlocked.CompareExchange(ref _locked, 1, 0) == 1) Thread.Yield();
	}

	public void Unlock()
	{
		_locked = 0;
	}
}