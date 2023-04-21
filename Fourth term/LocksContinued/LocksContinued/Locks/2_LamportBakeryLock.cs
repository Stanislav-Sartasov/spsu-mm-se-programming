using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Locks
{
    class LamportBakeryLock : ILock
    {
        volatile bool[] flags;
        volatile int[] labels;

        public LamportBakeryLock(int n)
        {
            flags = new bool[n];
            labels = new int[n];
            for (int i = 0; i < n; i++)
            {
                Volatile.Write(ref flags[i], false);
                labels[i] = 0;
            }
        }

        public void Lock()
        {
            int i = ThreadID.Get();
            flags[i] = true;
            labels[i] = labels.Max() + 1;

            while (labels.Select((label, index) =>

                (index != i) &&
                flags[index] &&
                (
                (labels[index] < labels[i]) 
                    || ((labels[index] == labels[i]) && index < i))
                )

            .Any(x=>x)) { };
        }

        public void Unlock()
        {
            flags[ThreadID.Get()] = false;
        }
    }
}
