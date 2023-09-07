using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_1.Collections
{
    public class TTASLock
    {
        volatile int state = 0;

        public void Lock()
        {
            while (true)
            {
                while (state == 1) {
                     Thread.Sleep(0);
                };
                if (Interlocked.CompareExchange(ref state, 1, 0) == 0)
                    return;
            }
        }

        public void Unlock()
        {
            state = 0;
        }
    }
}