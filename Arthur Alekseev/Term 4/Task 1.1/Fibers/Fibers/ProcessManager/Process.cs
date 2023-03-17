namespace Fibers.ProcessManager;

public class Process
{
	private const int LongPauseBoundary = 10000;
	private const int ShortPauseBoundary = 100;
	private const int WorkBoundary = 1000;
	private const int IntervalsAmountBoundary = 10;

	public const int PriorityLevelsNumber = 10;
	private static readonly Random Rng = new();

	private readonly List<int> _pauseIntervals = new();
	private readonly List<int> _workIntervals = new();

	public Process()
	{
		var amount = Rng.Next(IntervalsAmountBoundary);

		for (var i = 0; i < amount; i++)
		{
			_workIntervals.Add(Rng.Next(WorkBoundary));
			_pauseIntervals.Add(Rng.Next(
				Rng.NextDouble() > 0.9
					? LongPauseBoundary
					: ShortPauseBoundary));
		}

		Priority = Rng.Next(PriorityLevelsNumber);
	}

	public int Priority { get; }

	public int TotalDuration => ActiveDuration + _pauseIntervals.Sum();

	public int ActiveDuration => _workIntervals.Sum();

	public void Run()
	{
		for (var i = 0; i < _workIntervals.Count; i++)
		{
			Thread.Sleep(_workIntervals[i]); // work emulation
			var pauseBeginTime = DateTime.Now;
			do
			{
				ProcessManager.Switch(false);
			} while ((DateTime.Now - pauseBeginTime).TotalMilliseconds < _pauseIntervals[i]); // I/O emulation
		}

		ProcessManager.Switch(true);
	}
}