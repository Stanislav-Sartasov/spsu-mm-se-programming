using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Consumer<T>
    {
        private List<T> buffer;
        private Action<T> consume;
        volatile bool stop = true;
        private Thread thread;

        public Consumer(List<T> buffer, Action<T> consume)
        {
            this.buffer = buffer;
            this.consume = consume;
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
                TTASLock.Lock();

                if (buffer.Count > 0)
                {
                    T item = buffer[0];
                    buffer.RemoveAt(0);
                    
                    TTASLock.Unlock();

                    consume(item);
                }
                else
                {
                    TTASLock.Unlock();
                }
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
