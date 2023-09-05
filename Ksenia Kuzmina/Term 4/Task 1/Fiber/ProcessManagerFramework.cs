using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace Fiber
{
    public static class ProcessManager
    {
        private static List<Fiber> Fibers = new();
        private static bool Prio;
        private static Fiber? CurrentFiber;
        private static int CurrentId;
        private static Random Random = new Random();

        public static void Run(List<Process> procs, bool prio)
        {
            Prio = prio;

            foreach (Process process in procs) 
            {
                Fibers.Add(new Fiber(process.Run, process.Priority));
            }

            CurrentFiber = Fibers[0];
            Fiber.Switch(CurrentFiber.Id);
        }

        public static void Switch(bool fiberFinished)
        {
            if (fiberFinished) 
            { 
                Fibers.Remove(CurrentFiber); 
                if (Fibers.Count == 0) 
                {

                    Console.WriteLine("Switch to primary ID");
                    Fiber.Switch(Fiber.PrimaryId);
                }
            }

            CurrentFiber = Choose();

            Fiber.Switch(CurrentFiber.Id);
        }

        private static Fiber Choose()
        {
            if (Prio)
            {
                int maxPrio = Fibers.Select(x => x.Prio).Max();
                var maxPrioFibers = Fibers.FindAll(f => f.Prio == maxPrio);

                if (Random.NextSingle() < 0.1)
                {
                    return Fibers[Random.Next() % Fibers.Count];
                }

                return maxPrioFibers[Random.Next() % maxPrioFibers.Count];
            }
            else
            {
                CurrentId = (CurrentId + 1) % Fibers.Count;
                return Fibers[CurrentId];
            }  
        }
    }
}