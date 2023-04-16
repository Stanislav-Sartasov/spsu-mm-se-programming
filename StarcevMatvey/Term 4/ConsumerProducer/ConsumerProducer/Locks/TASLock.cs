using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumerProducer.Locks;

namespace ConsumerProducer
{
    public class TASLock : ILock
    {
        private volatile int stop;

        public TASLock()
        {
            stop = 0;
        }

        public void Lock()
        {
            while (Interlocked.CompareExchange(ref stop, 1, 0) == 1)
            {

            }
        }

        public void Unlock()
        {
            stop = 0;
        }
    }
}
