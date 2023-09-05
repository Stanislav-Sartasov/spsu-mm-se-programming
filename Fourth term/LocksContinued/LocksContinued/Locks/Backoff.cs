using System;
using System.Threading;

namespace LocksContinued.Locks
{
    public class Backoff
    {
        readonly int minDelay, maxDelay;
        int limit;
        readonly Random random;

        public Backoff(int min, int max)
        {
            minDelay = min;
            maxDelay = max;
            limit = minDelay;
            random = new Random();
        }

        public void DoBackoff()
        {
            int delay = random.Next(limit);
            limit = Math.Min(maxDelay, 2 * limit);
            Thread.Sleep(delay);
        }
    }
}
