namespace Fibers;

public class ProcessManager : IDisposable
{
    private Func<List<ProcessData>, uint> strategy;

    public bool IsRunning { get; private set; }

    private readonly Dictionary<Process, Fiber> fibersByProcess;
    private readonly List<Fiber> finishedFibers;
    private Process? currProcess;

    public ProcessManager(Func<List<ProcessData>, uint> strategy)
    {
        this.strategy = strategy;
        IsRunning = false;
        fibersByProcess = new Dictionary<Process, Fiber>();
        finishedFibers = new List<Fiber>();
        currProcess = null;
    }


    public void AddTask(Process process)
    {
        fibersByProcess[process] = new Fiber(process.Run);
    }

    public void Run()
    {
        if (IsRunning || !fibersByProcess.Any()) return;
        IsRunning = true;
        Switch(isFiberFinished: false);
    }


    internal void Switch(bool isFiberFinished)
    {
        if (!IsRunning) return;

        if (isFiberFinished && currProcess != null)
        {
            Console.WriteLine($"Fiber {fibersByProcess[currProcess].Id} is finished");
            var currFiber = fibersByProcess[currProcess];
            fibersByProcess.Remove(currProcess);
            finishedFibers.Add(currFiber);

            if (!fibersByProcess.Any())
            {
                Console.WriteLine($"Primary fiber {Fiber.PrimaryId} is done");
                Fiber.Switch(Fiber.PrimaryId);
                return;
            }
        }

        var processesData = fibersByProcess.Select(kv =>
        {
            var (pr, fib) = kv;
            return new ProcessData(fib.Id, pr.Priority, pr.ActiveDuration, pr.TotalDuration);
        }).ToList();

        var prev = currProcess;
        var nextId = strategy(processesData);
        currProcess = fibersByProcess.ToList().First(kv => kv.Value.Id == nextId).Key;

        // Console.WriteLine($"Fiber {fibersByProcess[currProcess].Id} with priority {currProcess.Priority} is running");

        if (currProcess != prev)
        {
            Fiber.Switch(fibersByProcess[currProcess].Id);
        }
    }


    public void Dispose()
    {
        foreach (var fiber in fibersByProcess.Values) fiber.Delete();
        foreach (var fiber in finishedFibers) fiber.Delete();
        fibersByProcess.Clear();
        finishedFibers.Clear();
        currProcess = null;
        IsRunning = false;
    }
}