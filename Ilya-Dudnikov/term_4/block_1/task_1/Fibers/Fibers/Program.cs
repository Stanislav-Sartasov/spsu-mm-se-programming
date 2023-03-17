using Fibers.Fibers;
using Fibers.ProcessManager;
using static Fibers.ProcessManager.ProcessManager;


namespace Fibers;

internal class Program
{
    private static void HelpMessage()
    {
        Console.WriteLine("Usage: first argument is the number of fibers to be ran" + Environment.NewLine +
                          "second argument is the fiber scheduler to be used. Options for scheduler: " + Environment.NewLine +
                          "- Default: default fiber scheduler that uses trivial FIFO policy" + Environment.NewLine + 
                          "- Priority: priority-based scheduling with aging technique" + Environment.NewLine);
    }
    
    private static int Main(string[] args)
    {
        if (args.Length < 2)
        {
            HelpMessage();
            return 0;
        }

        int processesCount;
        if (int.TryParse(args[0], out processesCount))
        {
            var schedulerType = args[1];
            FiberScheduler scheduler;

            if (schedulerType == "Default")
            {
                scheduler = new DefaultScheduler();
            }
            else if (schedulerType == "Priority")
            {
                scheduler = new PriorityScheduler();
            }
            else
            {
                HelpMessage();
                return 0;
            }
        
            var processList = new List<Process>();
            for (var i = 0; i < processesCount; i++) processList.Add(new Process());

            Run(processList, scheduler);
        }
        else
        {
            HelpMessage();
        }
        
        return 0;
    }
}