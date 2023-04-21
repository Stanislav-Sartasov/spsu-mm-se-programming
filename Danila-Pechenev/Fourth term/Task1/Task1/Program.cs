using ProcessManager;

namespace Task1;

class Program
{
    static int Main(string[] args)
    {
        int N = 4;  // Number of processes

        var processes = new List<Process>();
        for (int i = 0; i < N; i++)
        {
            processes.Add(new Process());
        }

        Console.WriteLine("Running with taking priorities into account.");
        ProcessManager.ProcessManager.Run(processes, withPriority: true);  // Run with taking priorities into account

        Console.WriteLine();

        Console.WriteLine("Running without taking priorities into account.");
        ProcessManager.ProcessManager.Run(processes, withPriority: false);  // Run without taking priorities into account

        return 0;
    }
}
