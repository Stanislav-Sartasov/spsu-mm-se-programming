using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Locks
{
    public class HBOLock : ILock
    {
        const int LOCAL_MIN_DELAY = 1;
        const int LOCAL_MAX_DELAY = 10;
        const int REMOTE_MIN_DELAY = 100;
        const int REMOTE_MAX_DELAY = 1000;
        const int FREE = -1;
        volatile int state = FREE;

        public void Lock()
        {
            int myCluster = ThreadID.GetCluster();
            Backoff localBackoff = new Backoff(LOCAL_MIN_DELAY, LOCAL_MAX_DELAY);
            Backoff remoteBackoff = new Backoff(REMOTE_MIN_DELAY, REMOTE_MAX_DELAY);
            while (true)
            {
                if (Interlocked.CompareExchange(ref state, myCluster, FREE) == FREE)
                {
                    return;
                }
                int lockState = state;
                if (lockState == myCluster)
                {
                    localBackoff.DoBackoff();
                }
                else
                {
                    remoteBackoff.DoBackoff();
                }
            }
        }
        public void Unlock()
        {
            state = FREE;
        }
    }
}
