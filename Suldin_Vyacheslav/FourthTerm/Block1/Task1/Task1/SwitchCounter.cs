using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public static class SwithCounter
    {
        private static readonly int frequency = 2;

        private static int counter = 0;

        public static void Increase()
        {
            counter = (counter + 1) % frequency;
        }

        public static void Reset()
        {
            counter = 0;
        }

        internal static void Work(List<FiberData> fibers, FiberData luckyFiber)
        {
            Increase();

            // showdown
            if (counter == 0)
            {
                foreach (var fiber in fibers)
                {
                    if (fiber.AFKPoints > FiberData.Barrier && fiber != luckyFiber)
                    {
                        fiber.PriorityUp();
                    }
                }
            }
            else
            {
                foreach (var fiber in fibers)
                {
                    if (fiber != luckyFiber)
                    {
                        fiber.AFKPoints += 1;
                    }
                }
            }
            luckyFiber.AFKPoints = 0;
        }
    }
}
