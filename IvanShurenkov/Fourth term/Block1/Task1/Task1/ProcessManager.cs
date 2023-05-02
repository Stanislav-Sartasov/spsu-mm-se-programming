using Fibers;
using System;
using System.Collections.Generic;

namespace ProcessManager
{
    public static class ProcessManager
    {
        const int maxPrio = 10;
        private static readonly Random Rng = new();

        public static ProcessManagerStrategy ProcessStrategy { get; private set; }

        private static List<Fiber>[] fibers = new List<Fiber>[maxPrio];
        private static List<Fiber> finishedFibers = new List<Fiber>();
        private static int currFiber = -1;
        private static int currPrio = -1;
        private static bool isEmpty = true;

        public static void Switch(bool fiberFinished)
        {
            bool hasNotFiber = (-1 == currFiber || -1 == currPrio);
            uint prevFiberId = (!hasNotFiber ? fibers[currPrio][currFiber].Id : 0);
            if (fiberFinished && !hasNotFiber)
            {
                finishedFibers.Add(fibers[currPrio][currFiber]);
                // Console.WriteLine("Fiber [{0}] Finished [{1}]", fibers[currPrio][currFiber].Id, finishedFibers.Count);
                fibers[currPrio].RemoveAt(currFiber);
            }

            currPrio = currFiber = -1;

            int nextFiber; 
            uint nextFiberId = Fiber.PrimaryId;
            switch (ProcessStrategy)
            {
                case ProcessManagerStrategy.Trivial:
                    isEmpty = (0 == fibers[0].Count);
                    if (isEmpty)
                    {
                        break;
                    }

                    currFiber = Rng.Next(fibers[0].Count);
                    currPrio = (0 == fibers[0].Count? -1 : 0);

                    nextFiberId = fibers[0][currFiber].Id;
                    break;
                case ProcessManagerStrategy.Priority:
                    int prioSum = 0;
                    for (int i = 0; i < maxPrio; i++)
                    {
                        prioSum += fibers[i].Count * (i + 1);
                    }

                    isEmpty = (0 == prioSum);
                    if (isEmpty)
                    {
                        break;
                    }

                    nextFiber = Rng.Next(prioSum);

                    for (int i = 0; i < maxPrio; i++)
                    {
                        if (nextFiber < fibers[i].Count * (i + 1))
                        {
                            currFiber = nextFiber / (i + 1);
                            currPrio = i;
                            break;
                        }
                        nextFiber -= fibers[i].Count * (i + 1);
                    }
                    nextFiberId = fibers[currPrio][currFiber].Id;
                    break;
            }
            if (0 != nextFiberId && (hasNotFiber || prevFiberId != nextFiberId))
            {
                Fiber.Switch(nextFiberId);
            }
        }

        private static void AddTask(Process process)
        {
            switch (ProcessStrategy)
            {
                case ProcessManagerStrategy.Trivial:
                    fibers[0].Add(new Fiber(process.Run));
                    break;
                case ProcessManagerStrategy.Priority:
                    int prio = process.Priority % maxPrio;
                    fibers[prio].Add(new Fiber(process.Run));
                    break;
            }
        }

        public static void Run(List<Process> processes, ProcessManagerStrategy strategy=ProcessManagerStrategy.Trivial)
        {
            ProcessStrategy = strategy;

            for (int i = 0; i < maxPrio; i++)
            {
                if (fibers[i] == null)
                    fibers[i] = new List<Fiber>();
            }
            ClearFibers();

            for (int i = 0; i < processes.Count; i++)
            {
                AddTask(processes[i]);
                isEmpty = false;
            }

            Switch(false);
        }

        public static void ClearFibers()
        {
            for (int i = 0; i < maxPrio; i++)
            {
                for (int j = 0; j < fibers[i].Count; j++)
                {
                    Fiber.Delete(fibers[i][j].Id);
                }
                fibers[i].Clear();
            }
            for (int i = 0; i < finishedFibers.Count; i++)
            {
                // Console.WriteLine("Fiber [{0}] Was Finished", finishedFibers[i].Id);
                Fiber.Delete(finishedFibers[i].Id);
            }
            finishedFibers.Clear();

            currPrio = currFiber = -1;
            isEmpty = true;
        }
    }
}