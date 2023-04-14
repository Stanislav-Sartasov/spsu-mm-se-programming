using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerProducer
{
    public class Producer
    {
        private Thread thread;
        private List<Data<string>> tasks;
        private volatile bool stop;

        public Producer(List<Data<string>> tasks, string threadName)
        {
            thread = new Thread(Work);
            thread.Name = threadName;
            this.tasks = tasks;
            stop = false;
            thread.Start();
        }

        private void Work()
        {
            var name = Thread.CurrentThread.Name;

            while (!stop)
            {
                TASLock.Lock();
                tasks.Add(new Data<string>(name));
                TASLock.Unlock();

                Thread.Sleep(1000);
            }
        }

        private void Stop()
        {
            stop = true;
        }

        public void Join()
        {
            Stop();
            thread.Join();
        }
    }
}
