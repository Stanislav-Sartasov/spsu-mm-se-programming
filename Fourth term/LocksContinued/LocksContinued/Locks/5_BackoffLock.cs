using System.Threading;

namespace LocksContinued.Locks
{
    partial class BackoffLock : ILock
    {
        private volatile int state = 0;
        const int MIN_DELAY = 10;
        const int MAX_DELAY = 1000;

        public void Lock()
        {
            Backoff backoff = new Backoff(MIN_DELAY, MAX_DELAY);

            while (true)
            {
                while (state == 1) { };

                if (Interlocked.CompareExchange(ref state, 1, 0) == 0)
                {
                    return;
                }

                backoff.DoBackoff();
            }
        }
        public void Unlock()
        {
            state = 0;
        }
    }
}
