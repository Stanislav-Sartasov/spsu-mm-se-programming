using Fibers.ProcessManager;
using static Fibers.ProcessManager.ProcessManager;


namespace Fibers;

internal class Program
{
    private static int Main(string[] args)
    {
        var processList = Enumerable.Repeat(new Process(), 5).ToList();

        Run(processList);

        return 0;
    }
}