namespace ProducerConsumer;

public class Producer : Member
{
	private readonly Random _random = new();

	public Producer(Semaphore sem, List<int> list, string name, CancellationTokenSource tokenSrc)
		: base(sem, list, name, tokenSrc)
	{
	}

	protected override void Act()
	{
		int number = _random.Next(1000);
		list.Add(number);
		Console.WriteLine($"{Thread.CurrentThread.Name} added {number}");
	}
}