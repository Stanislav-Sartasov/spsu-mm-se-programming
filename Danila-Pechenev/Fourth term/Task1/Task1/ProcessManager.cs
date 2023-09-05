using Fibers;

namespace ProcessManager;

public static class ProcessManager
{
    public static bool WithPriority { get; set; }

    private static readonly Random Rng = new Random();

    private static List<Fiber> fibers = new List<Fiber>();

    private static List<Fiber> fibersToDelete = new List<Fiber>();

    private static List<int> priorities = new List<int>();

    private static int prioritySum = 0;

    private static int currentFiber = 0;

    public static void Run(List<Process> processes, bool withPriority)
    {
        WithPriority = withPriority;
        Initialize(processes);

        Fiber.Switch(fibers[0].Id);  // Run processes in fibers

        // Delete all other fibers:
        foreach (var fiber in fibersToDelete)
        {
            Fiber.Delete(fiber.Id);
        }
    }

    public static void Switch(bool fiberFinished)
    {
        if (fiberFinished)
        {
            fibersToDelete.Add(fibers[currentFiber]);
            fibers.RemoveAt(currentFiber);
            prioritySum -= priorities[currentFiber];
            priorities.RemoveAt(currentFiber);
        }

        if (fibers.Count == 0)
        {
            Fiber.Switch(Fiber.PrimaryId);
        }

        int nextFiber = 0;
        if (WithPriority)
        {
            double randomN = Rng.NextDouble();
            double currentN = 0;
            nextFiber = -1;
            for (int i = 0; !(currentN <= randomN && randomN <= currentN + (double)(priorities[i]) / prioritySum); i++)
            {
                nextFiber = i;
                currentN += (double)(priorities[i]) / prioritySum;
            }

            nextFiber++;
        }
        else
        {
            nextFiber = Rng.Next(fibers.Count);
        }

        currentFiber = nextFiber;
        Fiber.Switch(fibers[currentFiber].Id);
    }

    private static void Initialize(List<Process> processes)
    {
        processes.OrderByDescending(x => x.Priority);  // Sort processes (and, then, fibers) by priority

        foreach (var process in processes)
        {
            fibers.Add(new Fiber(process.Run));
            priorities.Add(convertPriority(process.Priority));
            prioritySum += convertPriority(process.Priority);
        }
    }

    private static int convertPriority(int priority)
    {
        return priority + 1;  // We do it in order not to divide by zero
    }
}
