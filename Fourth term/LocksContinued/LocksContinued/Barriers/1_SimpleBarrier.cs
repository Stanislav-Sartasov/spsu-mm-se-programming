using LocksContinued.Interfaces;
using System.Threading;

namespace LocksContinued.Barriers
{
    public class SimpleBarrier : IBarrier
    {
        volatile int count;
        int size;
        public SimpleBarrier(int n)
        {
            count = n;
            size = n;
        }

        public void Await()
        {
            int position = Interlocked.Decrement(ref count);
            if (position == 0)
            {
                count = size;
            }
            else
            {
                while (count != 0) { };
            }
        }
    }
}
