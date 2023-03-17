using Fibers;

namespace ProcessManager
{
    public static class ProcessManager
    {
        private static SchedulerStrategy schedulerStrategy;

        private static List<FiberWithPriority> fibers = new();
        private static FiberWithPriority currentFiber;

        public static void Switch(bool fiberFinished)
        {
            if (fiberFinished)
            {
                fibers.Remove(currentFiber);
            }

            if (!fibers.Any())
            {
                Thread.Sleep(1);
                Fiber.Switch(Fiber.PrimaryId);
            }

            currentFiber = NextFiber();

            Thread.Sleep(1);
            Fiber.Switch(currentFiber.Id);
        }

        private static FiberWithPriority NextFiber()
        {
            if (schedulerStrategy == SchedulerStrategy.NoPriority)
            {
                var rnd = new Random();
                return fibers[rnd.Next(fibers.Count)];
            }

            if (schedulerStrategy == SchedulerStrategy.WithPriority)
            {
                var squaredPriorities = fibers
                    .Select(p => Convert.ToInt32(Math.Pow(p.Priority, 2)))
                    .ToList();
                var prioritiesSum = squaredPriorities.Sum();

                var rnd = new Random().Next(prioritiesSum + 1);
                var current = 0;

                while (rnd > squaredPriorities[current])
                {
                    rnd -= squaredPriorities[current];
                    current++;
                }

                return fibers[current];
            }

            throw new InvalidDataException("Unknown scheduler strategy");
        }

        public static void Execute(List<Process> processes, SchedulerStrategy schedulerStrat)
        {
            schedulerStrategy = schedulerStrat;

            foreach (var process in processes)
            {
                fibers.Add(new FiberWithPriority(process.Run, process.Priority));
            }
            fibers = fibers.OrderByDescending(x => x.Priority).ToList();

            Switch(false);

            foreach (var fiber in fibers)
            {
                Fiber.Delete(fiber.Id);
            }
        }
    }
}
