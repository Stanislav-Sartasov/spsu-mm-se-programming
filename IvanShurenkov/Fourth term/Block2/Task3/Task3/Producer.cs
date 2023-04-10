using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Producer<T>
    {
        private List<T> buffer;
        private volatile bool stop = true;
        private Func<T> produce;
        private int timeout = 10;
        private Thread thread;

        public Producer(List<T> buffer, Func<T> produce)
        {
            this.buffer = buffer;
            this.produce = produce;
            this.thread = new Thread(Run);
        }

        public bool getState()
        {
            return stop;
        }

        private void Run()
        {
            while (!stop)
            {
                T item = produce();

                TTASLock.Lock();

                buffer.Add(item);

                TTASLock.Unlock();

                Thread.Sleep(timeout);
            }
        }

        public void Start()
        {
            stop = false;
            thread.Start();
        }

        public void Stop()
        {
            stop = true;
            thread.Join();
        }
    }
}
