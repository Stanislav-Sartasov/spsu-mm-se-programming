using Fibers.ProcessManager;

namespace Fibers
{
    public static class ProcessSimulator
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello! You can launch my process scheduler!\n" +
                              "Specify the number of processes as the first argument and the scheduler " +
                              "type as the second\n" +
                              "Available schedulers: Circle and Dynamic!\n");

            if (args.Length != 2)
            {
                Console.WriteLine("You should specify two arguments. Try again, please!");
                return;
            }

            if (!int.TryParse(args[0], out var processNumber) || processNumber < 1)
            {
                Console.WriteLine("First argument must be positive number. Try again, please!");
                return;
            }

            IScheduler scheduler;

            if (string.Equals(args[1], "Circle"))
                scheduler = new CircleScheduler();
            else if (string.Equals(args[1], "Dynamic"))
                scheduler = new DynamicPriorityScheduler();
            else
            {
                Console.WriteLine("Wrong scheduler type. Try again, please!");
                return;
            }

            ProcessManager.ProcessManager.ChangeScheduler(scheduler);
            
            for (var i = 0; i < processNumber; i++)
                scheduler.Add(new Process());

            Console.WriteLine($"Starting {args[1]} Scheduler on {processNumber} processes");
            ProcessManager.ProcessManager.Run();
            Console.WriteLine("Simulation ended successfully!");
        }
    }
}
