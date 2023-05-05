namespace ThreadPool;

public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("This program implements a thread pool.");

		var task = () =>
		{
			Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has started task");
			Thread.Sleep(new Random().Next(1000));
			Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has finished task");
		};

		var threadPool = new ThreadPool();

		for (int i = 0; i < 10; i++)
			threadPool.Enqueue(task);

		Thread.Sleep(10000);

		threadPool.Dispose();
	}
}