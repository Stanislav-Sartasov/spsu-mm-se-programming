using System.Diagnostics;
using NUnit.Framework;
using Fibers;

namespace Fibers.Tests
{
    public class FibersTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Timeout(60000)]
        public void TestSimpleStrategy()
        {
            List<Process> processes = new List<Process>();
            for (int i = 0; i < 5; i++)
            {
                Process newProcess = new Process();
                processes.Add(newProcess);
            }

            for (int i = 0; i < 5; i++)
            {
                ProcessManager.AddProcess(processes[i]);
            }

            ProcessManager.Start(false);

            Assert.Pass();
        }

        [Test, Timeout(60000)]
        public void TestPriorityStrategy()
        {
            List<Process> processes = new List<Process>();
            for (int i = 0; i < 5; i++)
            {
                Process newProcess = new Process();
                processes.Add(newProcess);
            }

            for (int i = 0; i < 5; i++)
            {
                ProcessManager.AddProcess(processes[i]);
            }

            ProcessManager.Start(true);

            Assert.Pass();
        }
    }
}