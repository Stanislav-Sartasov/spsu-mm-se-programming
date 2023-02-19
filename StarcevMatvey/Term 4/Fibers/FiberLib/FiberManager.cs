using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiberLib
{
    public class FiberManager
    {
        private readonly int maxPrio = 10;
        private List<FiberData> fibers;
        private List<FiberData>[] prioFibers;
        public int Count { get; private set; }
        private int nextId;

        public FiberManager()
        {
            this.fibers = new List<FiberData>();
            this.prioFibers = new List<FiberData>[maxPrio];
            this.Count = 0;
            this.nextId = 0;
        }

        public void Add(Process procces)
        {
            var fiberData = new FiberData(procces.Priority, nextId, new Fiber(procces.Run));
            fibers.Add(fiberData);
            prioFibers[fiberData.Prio % maxPrio].Add(fiberData);
            nextId++;
            Count++;
        }

        public void Remove(FiberData current)
        {
            fibers.Remove(current);
            prioFibers[current.Prio % maxPrio].Remove(current);
            Count--;
        }

        public void GetNextFiber(SchedulerPriority prio)
        {
            switch (prio)
            {
                case SchedulerPriority.NonePrio:
                    Remove(fibers.First());
                    return;
                case SchedulerPriority.LevelPrio:
                    for (int i = maxPrio; i >= 0; i--)
                    {
                        if (prioFibers[i].Count != 0)
                        {
                            Remove(prioFibers[i].First());
                            return;
                        }
                    }

                    return;
            }
        }
    }
}
