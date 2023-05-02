using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessManager;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 3;

            List<Process> processes = new();
            /*
            for (int i = 0; i < n; i++)
            {
                processes.Add(new Process());
            }

            Console.WriteLine("With priority:");
            ProcessManager.ProcessManager.Run(processes, ProcessManagerStrategy.Priority);
            */

            processes.Clear();
            for (int i = 0; i < n; i++)
            {
                processes.Add(new Process());
            }
            Console.WriteLine("Without priority:");
            ProcessManager.ProcessManager.Run(processes, ProcessManagerStrategy.Trivial);
        }
    }
}
