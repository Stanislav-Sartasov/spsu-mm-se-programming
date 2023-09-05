namespace ThreadPool;

public class ThreadPool : IDisposable
{
	private const int ThreadsNumber = 5;
	private readonly List<Thread> _threads = new();
	private readonly Queue<Action> _queue = new();

	private readonly object _sync = new();

	private readonly CancellationTokenSource _tokenSrc = new();
	private bool IsDisposed => _tokenSrc.Token.IsCancellationRequested;

	public ThreadPool()
	{
		for (int i = 0; i < ThreadsNumber; i++)
		{
			var thread = new Thread(Run);
			_threads.Add(thread);
			thread.Start();
		}
	}

	private void Run()
	{
		while (true)
		{
			Action current;
			lock (_sync)
			{
				while (_queue.Count == 0 && !IsDisposed)
					Monitor.Wait(_sync);

				if (IsDisposed) return;

				current = _queue.Dequeue();
			}

			current.Invoke();
		}
	}

	public void Enqueue(Action a)
	{
		if (IsDisposed)
			throw new ObjectDisposedException("Thread pool is disposed");

		lock (_sync)
		{
			_queue.Enqueue(a);
			Monitor.Pulse(_sync);
		}
	}

	public void Dispose()
	{
		_tokenSrc.Cancel();

		lock (_sync)
		{
			_queue.Clear();
			Monitor.PulseAll(_sync);
		}

		foreach (var thread in _threads)
			thread.Join();
		_tokenSrc.Dispose();
	}
}