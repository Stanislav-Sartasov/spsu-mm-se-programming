using PCLib;

namespace PCApp;

public static class Program
{
	public static void Main(string[] args)
	{
		if (args.Length < 2) throw new Exception("Two arguments expected: producers amount and consumers amount.");

		var producersAmount = ArgumentsParser.Parse(args[0]); 
		var consumersAmount = ArgumentsParser.Parse(args[1]);

		var locker = new TASLock();
		var buffer = new List<string>();

		var producers = new List<Producer>();
		for (var i = 0; i < producersAmount; i++)
		{
			var p = new Producer(locker, buffer);
			producers.Add(p);
			p.Start();
		}

		var consumers = new List<Consumer>();
		for (var i = 0; i < consumersAmount; i++)
		{
			var c = new Consumer(locker, buffer);
			consumers.Add(c);
			c.Start();
		}

		Console.ReadKey();
		Console.WriteLine();

		Console.WriteLine("Stopping started...");

		foreach (var p in producers)
			p.Join();
		
		foreach (var c in consumers)
			c.Join();

		Console.WriteLine("All producers and consumers stopped successfully.");
	}
}
