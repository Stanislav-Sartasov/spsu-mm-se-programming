﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace FiberLib
{

    
    //public static class ProcessManager
    //{
    //    private static FiberManager fibers;
    //    private static FiberData currentFiber;
    //    private static Random randomer;
    //    private static bool isExecuting;
    //    private static List<Fiber> removeList;
    //    private static SchedulerPriority scheduleType;
    //    static ProcessManager()
    //    {
    //        removeList = new List<Fiber>();
    //        fibers = new FiberManager();
    //        randomer = new Random();
    //        scheduleType = SchedulerPriority.LevelPrio;
    //        isExecuting = false;
    //    }
    //    public static void Switch(bool fiberFinished)
    //    {
    //        Thread.Sleep(1);
    //        if (fiberFinished)
    //        {
    //            fibers.Remove(currentFiber);
    //            if (fibers.IsEmpty)
    //            {
    //                Console.WriteLine($"Primary fiber is done id: {Fiber.PrimaryId}");
    //                Thread.Sleep(1);
    //                Fiber.Switch(Fiber.PrimaryId);
    //                return;
    //            }
    //            if (!currentFiber.Fiber.IsPrimary)
    //            {
    //                removeList.Add(currentFiber.Fiber);
    //            }
    //            Console.WriteLine($"Fiber is done {currentFiber.Fiber.Id}");
    //        }
    //        currentFiber = fibers.GetNextFiber(scheduleType);
    //        if (currentFiber == null)
    //        {
    //            Console.WriteLine("OHHH.....");
    //            return;
    //        }
    //        Thread.Sleep(1);
    //        Fiber.Switch(currentFiber.Fiber.Id);
    //    }
    //    public static void Exec(SchedulerPriority priority)
    //    {
    //        if (isExecuting)
    //        {
    //            Console.WriteLine("allready executing, function is called second time");
    //            return;
    //        }
    //        if (fibers.IsEmpty)
    //        {
    //            Console.WriteLine("There is no processes to execute\n(try use AddProcess() function)");
    //            return;
    //        }
    //        scheduleType = priority;
    //        isExecuting = true;
    //        Switch(false);
    //    }
    //    public static void Add(Process process)
    //    {
    //        fibers.Add(process);
    //    }
    //    public static void Dispose()
    //    {
    //        fibers.Dispose();
    //        Console.WriteLine("Dispose done");
    //    }
    //}
    public static class ProcessManager
    {
        private static FiberManager fiberManager;
        private static FiberData currentFiberData;
        private static SchedulerPriority priority;

        private static bool isExecuting;

        static ProcessManager()
        {
            fiberManager = new FiberManager();
            priority = SchedulerPriority.NonePrio;
            isExecuting = false;
        }

        public static void Add(Process process)
        {
            fiberManager.Add(process);
        }

        public static void Dispose()
        {
            fiberManager.Dispose();
        }


        public static void Switch(bool fiberFinished)
        {
            if (fiberFinished)
            {
                fiberManager.Remove(currentFiberData);
                if (fiberManager.IsEmpty)
                {
                    Console.WriteLine($"Primary fiber {Fiber.PrimaryId} is done");
                    Fiber.Switch(Fiber.PrimaryId);
                    return;
                }

                Console.WriteLine($"Current fiber {currentFiberData.Id} is done");
            }

            currentFiberData = fiberManager.GetNextFiber(priority);

            if (currentFiberData == null)
            {
                throw new Exception("GetNextFiber returned null");
            }

            Fiber.Switch(currentFiberData.Fiber.Id);
        }

        public static void Exec(SchedulerPriority prio)
        {
            if (isExecuting)
            {
                Console.WriteLine("Already executing");
                return;
            }

            if (fiberManager.IsEmpty)
            {
                Console.WriteLine("No process to executing");
            }

            priority = prio;
            isExecuting = true;
            Switch(false);
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