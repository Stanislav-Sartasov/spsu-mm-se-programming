using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.WorkStealing
{
    class CircularArray
    {
        private int logCapacity;
        private Task[] currentTasks;

        public CircularArray(int myLogCapacity)
        {
            logCapacity = myLogCapacity;
            currentTasks = new Task[Capacity];
        }
        public int Capacity => 1 << logCapacity;

        public Task Get(int i)
        {
            return currentTasks[i % Capacity];
        }

        public void Put(int i, Task task)
        {
            currentTasks[i % Capacity] = task;
        }

        public CircularArray Resize(int bottom, int top)
        {
            CircularArray newTasks = new CircularArray(logCapacity + 1);
            for (int i = top; i < bottom; i++)
            {
                newTasks.Put(i, Get(i));
            }
            return newTasks;
        }
    }

    public class UnboundedDEQueue : IDEQueue<Task>
    {
        const int LOG_CAPACITY = 4;
        private volatile CircularArray tasks;
        volatile int bottom;
        volatile int top;
        public UnboundedDEQueue(int logCapacity)
        {
            tasks = new CircularArray(logCapacity);
            top = 0;
            bottom = 0;
        }
        public bool IsEmpty()
        {
            return (bottom <= top);
        }

        public void PushBottom(Task t)
        {
            int oldBottom = bottom;
            int oldTop = top;
            int size = oldBottom - oldTop;
            if (size >= tasks.Capacity - 1)
            {
                tasks = tasks.Resize(oldBottom, oldTop);
            }
            tasks.Put(oldBottom, t);
            bottom = oldBottom + 1;
        }

        public Task PopTop()
        {
            int oldTop = top;
            int newTop = oldTop + 1;
            int oldBottom = bottom;
            int size = oldBottom - oldTop;
            if (size <= 0) return null;
            Task t = tasks.Get(oldTop);
            if (Interlocked.CompareExchange(ref top, oldTop, newTop) == oldTop)
               return t;
            return null;
        }

        public Task PopBottom()
        {
            bottom--;
            int oldTop = top;
            int newTop = oldTop + 1;
            int size = bottom - oldTop;
            if (size < 0)
            {
                bottom = oldTop;
                return null;
            }
            Task t = tasks.Get(bottom);
            if (size > 0)
                return t;
            if (Interlocked.CompareExchange(ref top, oldTop, newTop) != oldTop)
                t = null;
            bottom = oldTop + 1;
            return t;
        }
    }
}
