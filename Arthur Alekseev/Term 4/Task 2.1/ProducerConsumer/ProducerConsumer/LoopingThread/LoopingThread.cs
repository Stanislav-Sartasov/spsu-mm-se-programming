using ProducerConsumer.Interfaces;

namespace ProducerConsumer.LoopingThread;

public class LoopingThread : IThread
{
	private readonly Thread _thread;
	private readonly Action _threadIteration;
	private volatile bool _isRunning;

	public LoopingThread(Action threadIteration)
	{
		_thread = new Thread(ThreadLoop);
		_threadIteration = threadIteration;
	}

	public void Start()
	{
		_isRunning = true;
		_thread.Start();
	}

	public void Stop()
	{
		_isRunning = false;
	}

	public void Wait()
	{
		Stop();
		_thread.Join();
	}

	private void ThreadLoop()
	{
		while (_isRunning) _threadIteration();
	}
}