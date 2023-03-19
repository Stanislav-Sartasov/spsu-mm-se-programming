using System;

namespace Fibers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            List<Process> processes = new List<Process>();
            
            for (int i = 0; i < n; i++)
            {
                Process newProcess = new Process();
                processes.Add(newProcess);
            }
            Process[] processesCopy = new Process[n];
            processes.CopyTo(processesCopy);

            for (int i = 0; i < n; i++)
            {
                ProcessManager.AddProcess(processes[i]);
            }

            Console.WriteLine("Strategy without priority");
            ProcessManager.Start(false);
            Console.WriteLine("Strategy without priority finished");

            for (int i = 0; i < n; i++)
            {
                ProcessManager.AddProcess(processesCopy[i]);
            }

            Console.WriteLine("Strategy with priority");
            ProcessManager.Start(true);
            Console.WriteLine("Strategy with priority finished");
        }

    }
}