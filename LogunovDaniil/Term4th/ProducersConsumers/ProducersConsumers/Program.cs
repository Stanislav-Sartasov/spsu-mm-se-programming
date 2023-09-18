using ProducersConsumers.Interfaces;

namespace ProducersConsumers;

public static class Program
{
	private static readonly int NumConsumers = 5;
	private static readonly int NumProducers = 5;

	private static void StartWorkers(List<AThreadedListAccessor> workers)
	{
		foreach (var worker in workers)
			worker.Start();
	}

	private static void StopWorkers(List<AThreadedListAccessor> workers)
	{
		foreach (var worker in workers)
			worker.Stop();
	}

	public static void Main(string[] args)
	{
		var locker = new AtomLock();
		var list = new List<int>();
		var workers = new List<AThreadedListAccessor>();

		for (int i = 0; i < NumConsumers; i++)
		{
			workers.Add(new Consumer(locker, list));
		}
		for (int i = 0; i < NumProducers; i++)
		{
			workers.Add(new Producer(locker, list));
		}

		Console.WriteLine("Starting all workers! To stop the process, press any key...");
		StartWorkers(workers);

		Console.ReadKey(true);

		Console.WriteLine("Stopping workers...");
		StopWorkers(workers);
		Console.WriteLine("Stopped all workers!");
	}
}