namespace ProducerConsumer;

public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("This program is a multithreaded implementation of the producer-consumer problem.");
		Console.WriteLine("Press any key to stop process.");

		int producerCount = 5;
		int consumerCount = 5;

		var list = new List<int>();
		var sem = new Semaphore(1, 1);
		var members = new List<Member>();
		var tokenSrc = new CancellationTokenSource();

		for (int i = 0; i < producerCount; i++)
			members.Add(new Producer(sem, list, $"Producer {i + 1}", tokenSrc));

		for (int i = 0; i < consumerCount; i++)
			members.Add(new Consumer(sem, list, $"Consumer {i + 1}", tokenSrc));

		foreach (var member in members)
			member.Start();

		Console.ReadKey(true);
		Console.WriteLine("Stopping...");

		tokenSrc.Cancel();

		foreach (var member in members)
			member.Stop();

		Console.WriteLine("Stopped!");
	}
}