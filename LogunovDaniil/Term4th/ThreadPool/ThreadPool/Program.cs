namespace ThreadPool;

public static class Program
{
	private static Random Rnd = new();
	private static int Id = 1;
	private static readonly int ActionNumber = 20;
	private static readonly int ActionMaxTime = 700;

	private static Action GetNextWork()
	{
		var personalId = Id;
		var workTime = Rnd.Next(100, ActionMaxTime);
		Id++;
		return () =>
		{
			Console.WriteLine($"Doing action #{personalId}!");
			Thread.Sleep(workTime);
		};
	}

	public static void Main(string[] args)
	{
		using (var pool = new ThreadPool())
		{
			for (int i = 0; i < ActionNumber; i++)
				pool.Enqueue(GetNextWork());

			Thread.Sleep(ActionMaxTime * ActionNumber + 1000);
		}
	}
}