using ProducerConsumer.Interfaces;

namespace ProducerConsumer.Consumer;

public class RandomFloatConsumer : IThread
{
	private readonly Guid _guid;
	private readonly ILock _lock;
	private readonly ILogger _logger;
	private readonly IThread _thread;

	private readonly List<float> _buffer;

	public RandomFloatConsumer(ILock lockObject, List<float> buffer, ILogger logger)
	{
		_buffer = buffer;
		_lock = lockObject;
		_thread = new LoopingThread.LoopingThread(ThreadJob);
		_guid = Guid.NewGuid();
		_logger = logger;
	}

	public void Start()
	{
		_logger.Log($"Starting consumer {_guid}");
		_thread.Start();
	}

	public void Stop()
	{
		_logger.Log($"Stopping consumer {_guid}");
		_thread.Stop();
	}

	public void Wait()
	{
		_thread.Wait();
		_logger.Log($"Stopped consumer {_guid}");
	}

	private void ThreadJob()
	{
		_lock.Lock();
		if (_buffer.Count > 0)
		{
			var item = _buffer.First();
			_buffer.RemoveAt(0);
			_lock.Unlock();
			Consume(item);
		}
		else
			_lock.Unlock();

		Thread.Sleep(1000);
	}

	private void Consume(float item)
	{
		_logger.Log($"Consumed {item} by {_guid}");
	}
}