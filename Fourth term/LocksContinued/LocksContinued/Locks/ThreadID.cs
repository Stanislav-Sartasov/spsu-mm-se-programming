using System;
using System.Threading;

namespace LocksContinued.Locks
{
    internal class ThreadID
    {
        internal static int Get()
        {
            // not right
            return Thread.CurrentThread.ManagedThreadId % 2;
        }

        internal static int GetCluster()
        {
            return 0;
        }
    }
}

