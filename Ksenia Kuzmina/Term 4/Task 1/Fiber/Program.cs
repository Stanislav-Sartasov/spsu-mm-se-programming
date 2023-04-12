using System;
using System.Runtime.InteropServices;

namespace Fiber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This program manages fibers.");
            List<Process> processes = new List<Process>();
            for (int i = 0; i < 5; i++)
            {
                processes.Add(new Process());
            }
            Console.WriteLine("With priority\n");

            ProcessManager.Run(processes, true);
            processes.Clear();

            for (int i = 0; i < 5; i++)
            {
                processes.Add(new Process());
            }

            Console.WriteLine("Without priority\n");
            ProcessManager.Run(processes, false);
        }
    }
}