using FiberLib;

namespace Fibers
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Process process = new Process();
                ProcessManager.Add(process);
            }

            ProcessManager.Exec(SchedulerPriority.NonePrio);
            ProcessManager.Dispose();
            Console.WriteLine("End of program");

            Console.ReadKey();
        }
    }
}

