using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LocksContinued.Locks
{
    class TASLock : ILock // test-and-set / compare-and-set / compare-exchange
    {
        volatile int state = 0;

        public void Lock()
        {
            while (Interlocked.CompareExchange(ref state, 1, 0) == 1) { }
        }

        public void Unlock()
        {
            state = 0;
        }
    }
}

