using Fibers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessManager;

namespace Task1
{
    public class FiberData
    {
        public Fiber Fiber;

        public static readonly int Barrier = 3;

        public int DynamicPriority;

        public int AFKPoints;

        public FiberData(Process process)
        {
            Fiber = new Fiber(process.Run);
            DynamicPriority = process.Priority;
            AFKPoints = 0;
        }

        public void PriorityUp()
        {
            if (DynamicPriority < 10)
            {
                DynamicPriority += 1;
            }
        }
    }
}
