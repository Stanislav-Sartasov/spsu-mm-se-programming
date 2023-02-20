using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.WorkStealing
{
    public class BDEQueue : IDEQueue<Task>
    {
        Task[] tasks;
        volatile int bottom;
        AtomicStampedReference<int> top;
        public BDEQueue(int capacity)
        {
            tasks = new Task[capacity];
            top = new AtomicStampedReference<int>(0, 0);
            bottom = 0;
        }
        public void PushBottom(Task t)
        {
            tasks[bottom] = t;
            Interlocked.Increment(ref bottom);
        }

        // called by thieves to determine whether to try to steal
        public bool IsEmpty()
        {
            int oldTop = top.GetReference();
            return bottom <= oldTop;
        }

        // for thief
        public Task PopTop() 
        {
            int oldStamp;
            int oldTop = top.Get(out oldStamp);

            if (bottom <= oldTop)
                return null;

            Task t = tasks[oldTop];

            int newTop = oldTop + 1;
            int newStamp = oldStamp + 1;
            
            if (top.CompareAndSet(oldTop, newTop, oldStamp, newStamp))
                return t;
            return null;
        }

        // for ordinary thread
        public Task PopBottom() 
        {
            if (bottom == 0)
                return null;
            Interlocked.Decrement(ref bottom);
            Task t = tasks[bottom];
            int oldStamp;
            int oldTop = top.Get(out oldStamp);
            if (bottom > oldTop)
                return t;

            int newStamp = oldStamp + 1;
            if (bottom == oldTop)
            {
                bottom = 0;
                if (top.CompareAndSet(oldTop, 0, oldStamp, newStamp))
                    return t;
            }
            top.Set(0, newStamp);
            return null;
        }
    }
}
