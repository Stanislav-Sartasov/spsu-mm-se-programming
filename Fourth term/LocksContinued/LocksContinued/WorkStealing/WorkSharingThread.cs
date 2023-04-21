using LocksContinued.Locks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.WorkStealing
{
    public class WorkSharingThread
    {
        Dictionary<int, Queue<Task>> queue;
        Random random;
        const int THRESHOLD = 42;
        public WorkSharingThread(Dictionary<int, Queue<Task>> myQueue)
        {
            queue = myQueue;
            random = new Random();
        }
        public void Run()
        {
            int me = Thread.CurrentThread.ManagedThreadId;
            while (true)
            {
                Task task;
                lock (queue[me])
                {
                    task = queue[me].Dequeue();
                }
                if (task != null) task.Start();
                int size = queue[me].Count;
                if (random.Next(size + 1) == size)
                {
                    int victim = queue.Keys.ToList()[random.Next(queue.Keys.Count)];
                    int min = (victim <= me) ? victim : me;
                    int max = (victim <= me) ? me : victim;
                    lock (queue[min])
                    {
                        lock (queue[max])
                        {
                            Balance(queue[min], queue[max]);
                        }
                    }
                }
            }
        }
        private void Balance(Queue<Task> q0, Queue<Task> q1)
        {
            var qMin = (q0.Count < q1.Count) ? q0 : q1;
            var qMax = (q0.Count < q1.Count) ? q1 : q0;
            int diff = qMax.Count - qMin.Count;
            if (diff > THRESHOLD)
                while (qMax.Count > qMin.Count)
                    qMin.Enqueue(qMax.Dequeue());
        }
    }
}
