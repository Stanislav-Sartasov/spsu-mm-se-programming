using ProducerConsumer.Interfaces;

namespace ProducerConsumer.Producer;

public class RandomFloatProducer : IThread
{
	private readonly Guid _guid;
	private readonly ILock _lock;
	private readonly ILogger _logger;
	private readonly Random _random;
	private readonly IThread _thread;

	private readonly List<float> _buffer;

	public RandomFloatProducer(ILock lockObject, List<float> buffer, ILogger logger)
	{
		_buffer = buffer;
		_lock = lockObject;
		_random = new Random();
		_thread = new LoopingThread.LoopingThread(ThreadIteration);
		_logger = logger;
		_guid = Guid.NewGuid();
	}

	public void Start()
	{
		_logger.Log($"Starting producer {_guid}");
		_thread.Start();
	}

	public void Stop()
	{
		_logger.Log($"Stopping producer {_guid}");
		_thread.Stop();
	}

	public void Wait()
	{
		_thread.Wait();
		_logger.Log($"Stopped producer {_guid}");
	}

	private void ThreadIteration()
	{
		var item = Produce();

		_lock.Lock();
		_buffer.Add(item);
		_lock.Unlock();

		Thread.Sleep(1000);
	}

	private float Produce()
	{
		var item = _random.NextSingle();
		_logger.Log($"Produced {item} by {_guid}");
		return item;
	}
}