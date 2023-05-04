namespace ProducerConsumer;

public class Consumer : Member
{
	public Consumer(Semaphore sem, List<int> list, string name, CancellationTokenSource tokenSrc)
		: base(sem, list, name, tokenSrc)
	{
	}

	protected override void Act()
	{
		if (list.Count == 0) return;

		int number = list[0];
		list.RemoveAt(0);
		Console.WriteLine($"{Thread.CurrentThread.Name} removed {number}");
	}
}