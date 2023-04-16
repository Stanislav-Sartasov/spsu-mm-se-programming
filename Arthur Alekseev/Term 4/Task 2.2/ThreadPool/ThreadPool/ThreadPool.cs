namespace ThreadPool;

public class ThreadPool : IDisposable
{
	private const int ThreadCount = 4;
	private readonly Thread[] _executors;
	private readonly object _lock;
	private readonly Queue<Action> _tasks;
	private volatile bool _running;

	public ThreadPool()
	{
		_running = true;
		_tasks = new Queue<Action>();
		_executors = new Thread[ThreadCount];

		_lock = new object();

		for (var i = 0; i < ThreadCount; i++)
		{
			_executors[i] = new Thread(ThreadWork);
			_executors[i].Start();
		}
	}

	public void Dispose()
	{
		_running = false;

		try
		{
			Monitor.Enter(_lock);
			_tasks.Clear();
			Monitor.PulseAll(_lock);
		}
		finally
		{
			Monitor.Exit(_lock);
		}

		Array.ForEach(_executors, x => x.Join());
	}

	public void Enqueue(Action task)
	{
		Monitor.Enter(_lock);
		try
		{
			_tasks.Enqueue(task);
			Monitor.PulseAll(_lock);
		}
		finally
		{
			Monitor.Exit(_lock);
		}
	}

	private void ThreadWork()
	{
		while (_running)
		{
			Action? next;
			Monitor.Enter(_lock);
			try
			{
				while (!_tasks.TryDequeue(out next) && _running)
					Monitor.Wait(_lock);
			}
			finally
			{
				Monitor.Exit(_lock);
			}

			next?.Invoke();
		}
	}
}