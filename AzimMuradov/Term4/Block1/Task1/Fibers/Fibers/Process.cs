namespace Fibers;

public class Process
{
    private static readonly Random Rng = new();


    private readonly ProcessManager pm;

    private const int LongPauseBoundary = 10000;

    private const int ShortPauseBoundary = 100;

    private const int WorkBoundary = 1000;

    private const int IntervalsAmountBoundary = 10;
    private const int PriorityLevelsNumber = 10;

    private readonly List<int> _workIntervals = new();
    private readonly List<int> _pauseIntervals = new();

    public Process(ProcessManager pm)
    {
        this.pm = pm;

        var amount = Rng.Next(IntervalsAmountBoundary);

        for (var i = 0; i < amount; i++)
        {
            _workIntervals.Add(Rng.Next(WorkBoundary));
            _pauseIntervals.Add(Rng.Next(Rng.NextDouble() > 0.9 ? LongPauseBoundary : ShortPauseBoundary));
        }

        Priority = Rng.Next(PriorityLevelsNumber);
    }

    public void Run()
    {
        for (var i = 0; i < _workIntervals.Count; i++)
        {
            Thread.Sleep(_workIntervals[i]); // work emulation
            var pauseBeginTime = DateTime.Now;
            do
            {
                pm.Switch(isFiberFinished: false);
                Thread.Sleep(30); // reduces work on PC
            } while ((DateTime.Now - pauseBeginTime).TotalMilliseconds < _pauseIntervals[i]); // I/O emulation
        }
        pm.Switch(isFiberFinished: true);
    }

    public int Priority { get; private set; }

    public int TotalDuration => ActiveDuration + _pauseIntervals.Sum();

    public int ActiveDuration => _workIntervals.Sum();
}