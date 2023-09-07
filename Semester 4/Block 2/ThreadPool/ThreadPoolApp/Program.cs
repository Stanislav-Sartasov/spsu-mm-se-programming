namespace ThreadPoolApp;

public class Program
{
	public static void Main(string[] args)
	{
		var task = () =>
		{
			Console.WriteLine($"Thread {Thread.CurrentThread.Name} has started task");
			Thread.Sleep(1000);
			Console.WriteLine($"Thread {Thread.CurrentThread.Name} has finished task");
		};

		using (var threadPool = new ThreadPool())
		{
			var taskAmount = 15;
			for (int i = 0; i < taskAmount; i++)
				threadPool.Enqueue(task);

			Thread.Sleep(10000);
		}
	}
}
