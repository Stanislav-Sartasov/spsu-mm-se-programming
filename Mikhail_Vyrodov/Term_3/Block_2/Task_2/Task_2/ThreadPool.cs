using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Action = System.Action;

namespace Task_2
{   public class ThreadPool : IDisposable
    {
        private Queue<Action> tasksQueue;
        private Thread[] threads; 
        private volatile bool disposeFlag = false;

        private object sync;

        public ThreadPool(int threadsCount)
        {
            sync = new object();
            threads = new Thread[threadsCount];
            tasksQueue = new Queue<Action>();

            for (int i = 0; i < threadsCount; i++)
            {
                threads[i] = new Thread(Run);
                threads[i].Start();
            }
        }

        public void Enqueue(Action a)
        {
            if (disposeFlag)
            {
                throw new Exception("Trying to call enqueue method when pool was already disposed.");
            }

            lock (sync)
            {
                tasksQueue.Enqueue(a);
                Monitor.Pulse(sync);
            }
        }

        public void Run()
        {
            while (!disposeFlag)
            {
                Action? task = null;
                lock (sync)
                {
                    while (tasksQueue.Count == 0 && !disposeFlag)
                    {
                        Monitor.Wait(sync);
                    }

                    if (disposeFlag)
                    {
                        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} was stopped.");
                        return;
                    }

                    task = tasksQueue.Dequeue();
                }

                if (task != null)
                    task();
            }
        }

        public void Dispose()
        {
            disposeFlag = true;

            lock (sync)
            {
                Monitor.PulseAll(sync);
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            tasksQueue.Clear();
            Console.WriteLine("Pool was disposed.");
        }
    }
}
