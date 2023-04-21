using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public class ThreadPool : IDisposable
    {
        private const int threadCount = 5;
        public Queue<Action> actions { get; private set; }
        public List<Thread> threads { get; private set; }
        private volatile bool stop = false;

        public ThreadPool()
        {
            actions = new Queue<Action>();
            threads = new List<Thread>();
            for (var i = 0; i < threadCount; i++)
            {
                threads.Add(new Thread(Operate));
            }
            for (var i = 0; i < threadCount; i++)
            {
                threads[i].Start();
            }
        }

        public void Enqueue(Action action)
        {
            if (stop)
            {
                throw new InvalidOperationException("Enqueue in disposed thread pool");
            }
            lock (actions)
            {
                actions.Enqueue(action);
                Monitor.Pulse(actions);
            }
        }

        private void Operate()
        {
            while (true)
            {
                Action action = null;
                lock (actions)
                {
                    if (!actions.Any() && stop)
                    {
                        break;
                    }
                    while (!actions.Any() && !stop)
                    {
                        Monitor.Wait(actions);
                    }
                    if (actions.Any())
                    {
                        action = actions.Dequeue();
                    }
                }

                if (action != null)
                {
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"exeption [{ex}]");
                    }
                }
            }
        }

        public void Dispose()
        {
            stop = true;

            lock (actions)
            {
                Monitor.PulseAll(actions);
            }

            for (var i = 0; i < threadCount; i++)
            {
                threads[i].Join();
            }

            threads.Clear();
        }
    }
}
