using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Monitors
{
    public class Semaphore
    {
        int capacity;
        int state;
        object sync = new object();

        public Semaphore(int c)
        {
            capacity = c;
            state = 0;

        }

        public void Acquire()
        {
            Monitor.Enter(sync);

            try
            {
                while (state == capacity)
                {
                    Monitor.Wait(sync);
                }
                state++;
            }
            finally
            {
                Monitor.Exit(sync);
            }
        }

        public void Release()
        {
            Monitor.Enter(sync);
            try
            {
                if (state == 0) throw new InvalidOperationException(); // or alternatively do nothing
                state--;
                Monitor.PulseAll(sync);
            }
            finally
            {
                Monitor.Exit(sync);
            }
        }
    }
}