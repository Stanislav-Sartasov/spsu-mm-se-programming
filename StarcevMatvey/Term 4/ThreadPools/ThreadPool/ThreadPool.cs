﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ThreadPool
{
    public class ThreadPool : IDisposable
    {
        private Queue<Action> actQueue;
        private List<Thread> threads;
        private volatile bool stop;

        public ThreadPool(int count)
        {
            actQueue = new Queue<Action>();
            stop = false;

            threads = new List<Thread>();
            for (var i = 0; i < count; i++) threads.Add(new Thread(Work));

            threads.ForEach(x => x.Start());
        }

        public void Enqueue(Action act)
        {
            Monitor.Enter(actQueue);
            try
            {
                actQueue.Enqueue(act);
                Monitor.PulseAll(actQueue);
            }
            finally
            {
                Monitor.Exit(actQueue);
            }
        }


        private void Work()
        {
            while (!stop)
            {
                Action act = null;
                var flag = false;

                Monitor.Enter(actQueue);

                try
                {
                    while (actQueue.Count == 0 && !stop) Monitor.Wait(actQueue);

                    if (actQueue.Count > 0)
                    {
                        act = actQueue.Dequeue();
                        flag = true;
                    }
                }
                finally
                {
                    Monitor.Exit(actQueue);
                }

                if (flag && act != null) act.Invoke();
            }
        }

        public void Dispose()
        {
            stop = true;

            Monitor.Enter(actQueue);
            try
            {
                actQueue.Clear();
                Monitor.PulseAll(actQueue);
            }
            finally
            {
                Monitor.Exit(actQueue);
            }

            threads.ForEach(x => x.Join());
            threads.Clear();
        }
    }
}
