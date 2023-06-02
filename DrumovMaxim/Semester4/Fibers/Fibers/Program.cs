using Fibers.Fibers;
using Fibers.ProcessManager;
/*using System.Diagnostics;*/
using static Fibers.ProcessManager.ProcessManager;

namespace Fibers;

internal class Program
{
    public static int printMessage(string message)
    {
        Console.WriteLine(message);
        return 0;
    }

    private static int Main(string[] args)
    {
        Console.WriteLine("Specify the number of fibers as the first argument and the scheduler " +
                          "type as the second\n" +
                          "Available schedulers: Default and Priority!\n");
        if (args.Length != 2)
        {
            return printMessage("You must enter two arguments. Try again!");
        }

        if (int.TryParse(args[0], out int processesCount) || processesCount < 1)
        {
            var schedulerType = args[1];
            IScheduler scheduler;

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
                return printMessage("Wrong scheduler type. Try again!");
            }

            var processList = new List<Process>();
            for (var i = 0; i < processesCount; i++) processList.Add(new Process());

            Run(processList, scheduler);
        }
        else
        {
            return printMessage("First argument must be positive number. Try again!");
        }
        return 0;
    }
}