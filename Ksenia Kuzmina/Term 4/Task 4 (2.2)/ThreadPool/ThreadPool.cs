using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadPool
{
    public class ThreadPool : IDisposable
    {
        private const int number = 5;
        private Queue<Action> queue;
        private List<Thread> threads;
        private volatile bool isRunning;

        public ThreadPool()
        {
            queue = new Queue<Action>();
            threads = new List<Thread>();
            isRunning = true;
            for (int i = 0; i < number; i++)
            {
                threads.Add(new Thread(Run));
                threads[i].Start();
            }
        }

        public void Enqueue(Action a)
        {
            Monitor.Enter(queue);

            try
            {
                queue.Enqueue(a);
                Monitor.Pulse(queue);
            }
            finally
            {
                Monitor.Exit(queue);
            }
        }

        private void Run()
        {
            while (isRunning)
            {
                Action action = null;

                Monitor.Enter(queue);
                try
                {
                    while ((queue.Count == 0) && (isRunning))
                    {
                        Monitor.Wait(queue);
                    }

                    if (queue.Count > 0)
                    {
                        action = queue.Dequeue();
                    }
                }
                finally
                {
                    Monitor.Exit(queue);
                }

                action?.Invoke();
            }
        }

        public void Dispose()
        {
            isRunning = false;

            Monitor.Enter(queue);
            try
            {
                queue.Clear();
                Monitor.PulseAll(queue);
            }
            finally
            {
                Monitor.Exit(queue);
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            threads.Clear();
        }
    }
}