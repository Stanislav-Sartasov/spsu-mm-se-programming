using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class TTASLock
    {
        static private volatile int state = 0;

        static public void Lock()
        {
            while (true)
            {
                while (1 == state)
                {
                    Thread.Sleep(0);
                }

                if (0 == Interlocked.CompareExchange(ref state, 1, 0))
                {
                    return;
                }
            }
        }

        static public void Unlock()
        {
            state = 0;
        }
    }
}
