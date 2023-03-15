using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using Fibers;
using System.Runtime.Intrinsics.Arm;
using Task1;
using System.Net;
using System.Security.Cryptography;

namespace ProcessManager
{
    public static class ProcessManager
    {
        public static Priority PriorityType;

        private static List<FiberData> fibers = new List<FiberData>();

        private static FiberData executingFiber;

        public static void Execute(List<Process> processes, Priority pt)
        {
            PriorityType = pt;
            fibers = processes.Select(pr => new FiberData(pr)).ToList();

            Switch(false);

            foreach (var fiberData in fibers)
            {
                Fiber.Delete(fiberData.Fiber.Id);
            }
        }

        public static void Switch(bool fiberFinished)
        {

            if (fiberFinished)
            {
                fibers.Remove(executingFiber);
            }

            if (!fibers.Any())
            {
                Fiber.Switch(Fiber.PrimaryId);
            }

            executingFiber = NextFiber();

            SwithCounter.Work(fibers, executingFiber);
            Fiber.Switch(executingFiber.Fiber.Id);
        }

        private static FiberData NextFiber()
        {
            switch (PriorityType)
            {
                case Priority.None:
                    Random random = new Random();
                    return fibers[random.Next(fibers.Count)];
                case Priority.Classic:
                    var topPrio = fibers.Where(x => x.DynamicPriority == fibers.Select(x => x.DynamicPriority).Max());
                    return topPrio.MaxBy(x => x.AFKPoints);
                default:
                    throw new InvalidOperationException("Unknown priority type");
            }
        }
    }

    public class Process
    {
        private static readonly Random Rng = new Random();

        private const int LongPauseBoundary = 10000;

        private const int ShortPauseBoundary = 100;

        private const int WorkBoundary = 1000;

        private const int IntervalsAmountBoundary = 10;
        private const int PriorityLevelsNumber = 10;

        private readonly List<int> _workIntervals = new List<int>();
        private readonly List<int> _pauseIntervals = new List<int>();

        public Process()
        {
            int amount = Rng.Next(IntervalsAmountBoundary);

            for (int i = 0; i < amount; i++)
            {
                _workIntervals.Add(Rng.Next(WorkBoundary));
                _pauseIntervals.Add(Rng.Next(
                        Rng.NextDouble() > 0.9
                            ? LongPauseBoundary
                            : ShortPauseBoundary));
            }
			
			Priority = Rng.Next(PriorityLevelsNumber);
        }

        public void Run()
        {
            for (int i = 0; i < _workIntervals.Count; i++)
            {
                Thread.Sleep(_workIntervals[i]); // work emulation
                DateTime pauseBeginTime = DateTime.Now;
                do
                {
                    ProcessManager.Switch(false);
                } while ((DateTime.Now - pauseBeginTime).TotalMilliseconds < _pauseIntervals[i]); // I/O emulation
            }
            ProcessManager.Switch(true);
        }

		public int Priority
		{
			get; private set;
		}
		
        public int TotalDuration
        {
            get
            {
                return ActiveDuration + _pauseIntervals.Sum();
            }
        }

        public int ActiveDuration
        {
            get
            {
                return _workIntervals.Sum();
            }
        }
    }
}
