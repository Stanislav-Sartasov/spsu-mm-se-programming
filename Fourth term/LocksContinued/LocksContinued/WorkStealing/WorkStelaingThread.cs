using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.WorkStealing
{
    public class WorkStealingThread
    {
        Dictionary<int, IDEQueue<Task>> queue;
        Random random;
        public WorkStealingThread(Dictionary<int, IDEQueue<Task>> myQueue)
        {
            queue = myQueue;
            random = new Random();
        }
        public void Run()
        {
            int me = Thread.CurrentThread.ManagedThreadId;
            Task task = queue[me].PopBottom();
            while (true)
            {
                while (task != null)
                {
                    // In fact we should run the workload here, Task.Start and Task.Wait are here for brevity
                    task.Start();
                    task.Wait();
                    task = queue[me].PopBottom();
                }
                while (task == null)
                {
                    Thread.Yield();
                    int victim = queue.Keys.ToList()[random.Next(queue.Keys.Count)];
                    if (!queue[victim].IsEmpty())
                    {
                        task = queue[victim].PopTop();
                    }
                }
            }
        }
    }
}
