namespace ThreadPool;

public static class Program
{
	public static void Main(string[] args)
	{
		void DisplayThread()
		{
			Console.WriteLine($"Called from {Environment.CurrentManagedThreadId}");
			Thread.Sleep(200);
		}

		using var pool = new ThreadPool();

		for (var i = 0; i < 20; i++)
			pool.Enqueue(DisplayThread);

		Thread.Sleep(30000);
	}
}