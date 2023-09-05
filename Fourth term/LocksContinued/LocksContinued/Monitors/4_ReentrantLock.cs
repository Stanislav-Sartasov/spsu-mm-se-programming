using System;
using System.Threading;

namespace LocksContinued.Monitors
{
    public class SimpleReentrantLock : ILock
    {
        object sync = new object();
        int owner, holdCount;
        public SimpleReentrantLock()
        {
            owner = 0;
            holdCount = 0;
        }

        public void Lock()
        {
            int me = Thread.CurrentThread.ManagedThreadId;
            Monitor.Enter(sync);
            try
            {
                if (owner == me)
                {
                    holdCount++;
                    return;
                }
                while (holdCount != 0)
                {
                    Monitor.Wait(sync);
                }
                owner = me;
                holdCount = 1;
            }
            finally
            {
                Monitor.Exit(sync);
            }
        }

        public void Unlock()
        {
            Monitor.Enter(sync);
            try
            {
                if (holdCount == 0 || owner != Thread.CurrentThread.ManagedThreadId)
                    throw new InvalidOperationException();
                holdCount--;
                if (holdCount == 0)
                {
                    Monitor.Pulse(sync);
                }
            }
            finally
            {
                Monitor.Exit(sync);
            }
        }
    }
}