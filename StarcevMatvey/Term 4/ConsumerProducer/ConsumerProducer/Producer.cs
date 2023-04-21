using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumerProducer.Locks;

namespace ConsumerProducer
{
    public class Producer
    {
        private Thread thread;
        private List<Data<string>> tasks;
        private volatile bool stop;
        private ILock locker;

        public Producer(ILock locker, List<Data<string>> tasks, string threadName)
        {
            this.locker = locker;

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
                locker.Lock();
                tasks.Add(new Data<string>(name));
                locker.Unlock();

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
