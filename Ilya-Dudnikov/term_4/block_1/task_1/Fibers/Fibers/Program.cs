using Fibers.Fibers;
using Fibers.ProcessManager;
using static Fibers.ProcessManager.ProcessManager;


namespace Fibers;

internal class Program
{
    private static int Main(string[] args)
    {
        var processList = new List<Process>();
        for (var i = 0; i < 5; i++) processList.Add(new Process());

        Run(processList, new PriorityScheduler());

        return 0;
    }
}