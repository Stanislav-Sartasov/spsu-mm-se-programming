using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerProducer
{
    public static class TASLock
    {
        private static volatile int stop = 0;

        public static void Lock()
        {
            while (Interlocked.CompareExchange(ref stop, 1, 0) == 1)
            {

            }
        }

        public static void Unlock()
        {
            stop = 0;
        }
    }
}
