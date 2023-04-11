using System;
using System.Collections.Generic;

namespace Task_1
{
    public class Producer<T>
    {
        private List<T> values;
        private int pauseInterval;
        private Func<T> produce;
        private Thread currThread;
        private volatile bool stopFlag;
        private Mutex mut;

        public void Run()
        {
            currThread = new Thread(new ThreadStart(CreateAndAddVal));
            stopFlag = false;
            currThread.Start();
        }

        public void Stop()
        {
            stopFlag = true;
            currThread.Join();
        }

        public void CreateAndAddVal()
        {
            
            while (!stopFlag)
            {
                T value = produce();
                bool isCaptured = mut.WaitOne();
                if (stopFlag)
                {
                    if (isCaptured)
                        mut.ReleaseMutex();
                    return;
                }
                values.Add(value);
                mut.ReleaseMutex();
                Thread.Sleep(pauseInterval);
            }
        }

        public Producer(int pauseInterval, List<T> values, Func<T> produce, Mutex mut) 
        {
            this.pauseInterval = pauseInterval;
            this.values = values;
            this.produce = produce;
            this.mut = mut;
        }

        public string ValuesToString()
        {
            string valuesStr = "";
            for (int i = 0; i < values.Count; i++)
            {
                if (i != 0)
                    valuesStr = valuesStr + " " + values[i].ToString();
                else
                    valuesStr = values[i].ToString();
            }
            return valuesStr;
        }
    }
}
