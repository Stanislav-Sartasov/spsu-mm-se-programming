using LocksContinued.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Barriers
{
    class SenseBarrier : IBarrier
    {
        volatile int count;
        int size;
        volatile bool phase = false;
        ThreadLocal<bool> threadPhase;

        public SenseBarrier(int n)
        {
            count = n;
            size = n;
            phase = false;
            threadPhase = new ThreadLocal<bool>(() => true);
        }

        public void Await()
        {
            bool myPhase = threadPhase.Value;
            int position = Interlocked.Decrement(ref count);
            if (position == 0)
            {
                count = size;
                phase = myPhase;
            }
            else
            {
                while (phase != myPhase) { }
            }
            threadPhase.Value = !myPhase;
        }
    }
}
