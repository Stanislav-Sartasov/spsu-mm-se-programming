using System;
using System.Collections.Generic;

namespace Task_1
{
    public class Consumer<T>
    {
        private List<T> values;
        private int pauseInterval;
        private Thread currThread;
        private volatile bool stopFlag;
        private Mutex mut;

        public void Run()
        {
            currThread = new Thread(new ThreadStart(TakeVal));
            stopFlag = false;
            currThread.Start();
        }

        public void Stop()
        {
            stopFlag = true;
            currThread.Join();
        }

        public void TakeVal()
        {
            while (!stopFlag)
            {
                bool isCaptured = mut.WaitOne();
                if (stopFlag)
                {
                    if (isCaptured)
                        mut.ReleaseMutex();
                    return;
                }
                if (isCaptured && values.Count == 0)
                {
                    mut.ReleaseMutex();
                    continue;
                }

                T x = values[0];
                values.RemoveAt(0);
                Console.WriteLine("Value {0} was removed", x);
                mut.ReleaseMutex();
                Thread.Sleep(pauseInterval);
            }
        }
            
        public Consumer(int pauseInterval, List<T> values, Mutex mut)
        {
            this.pauseInterval = pauseInterval;
            this.values = values;
            this.mut = mut;
        }

        public int GetValuesLen()
        {
            return values.Count;
        }
    }
}
