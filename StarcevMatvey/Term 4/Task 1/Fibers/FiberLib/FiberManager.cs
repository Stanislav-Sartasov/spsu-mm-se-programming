using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiberLib
{
    public class FiberManager : IDisposable
    {
        private readonly int maxPrio = 10;
        private List<FiberData> fibers;
        private List<FiberData>[] prioFibers;
        private FiberData temp;
        private bool IsHolded => temp != null;

        private int count;
        private int nextId;

        public bool IsEmpty => count == 0;

        public FiberManager()
        {
            this.fibers = new List<FiberData>();
            this.prioFibers = new List<FiberData>[maxPrio].Select(x => new List<FiberData>()).ToArray();

            temp = null;

            this.count = 0;
            this.nextId = 0;
        }

        public void Add(Process procces)
        {
            var fiberData = new FiberData(procces.Priority, nextId, new Fiber(procces.Run));
            fibers.Add(fiberData);
            prioFibers[fiberData.Prio % maxPrio].Add(fiberData);
            nextId++;
            count++;
        }

        public void Remove(FiberData current)
        {
            fibers.Remove(current);
            prioFibers[current.Prio % maxPrio].Remove(current);
            count--;
            temp = null;
        }

        private void Back()
        {
            if (IsHolded)
            {
                fibers.Add(temp);
                prioFibers[temp.Prio % maxPrio].Add(temp);
                temp = null;
            }
        }

        private void Takeout(FiberData fiberData)
        {
            if (IsHolded)
            {
                return;
            }
            
            temp = fiberData; 
            fibers.Remove(temp);
            prioFibers[temp.Prio % maxPrio].Remove(temp);

        }

        public FiberData GetNextFiber(SchedulerPriority prio)
        {
            FiberData data = null;

            if (IsEmpty)
            {
                return data;
            }

            if (count == 1)
            {
                Back();
            }

            switch (prio)
            {
                case SchedulerPriority.NonePrio:
                    data = fibers.First();
                    Back();
                    Takeout(data);
                    return data;

                case SchedulerPriority.LevelPrio:
                    for (var i = maxPrio - 1; i >= 0; --i)
                    {
                        if (prioFibers[i].Count != 0)
                        {
                            data = prioFibers[i].First();
                            Back();
                            Takeout(data);
                            return data;
                        }
                    }

                    return data;
            }

            return data;
        }

        public void Dispose()
        {
            foreach (var fiber in fibers) fiber.Fiber.Delete();
            temp.Fiber.Delete();

            fibers = new List<FiberData>();
            prioFibers = new List<FiberData>[maxPrio].Select(x => new List<FiberData>()).ToArray();

            count = 0;
            nextId = 0;

            temp = null;
        }
    }
}

