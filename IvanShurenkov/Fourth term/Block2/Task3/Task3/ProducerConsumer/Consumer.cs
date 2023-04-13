using Task3.Locks;

namespace Task3.ProducerConsumer
{
    public class Consumer<T>
    {
        private List<T> buffer;
        private Action<T> consume;
        private volatile bool stop = true;
        private Thread thread;
        private ILock locker;

        public Consumer(List<T> buffer, ILock locker, Action<T> consume)
        {
            this.buffer = buffer;
            this.consume = consume;
            thread = new Thread(Run);
            this.locker = locker;
        }

        public bool GetState()
        {
            return stop;
        }

        private void Run()
        {
            while (!stop)
            {
                locker.Lock();

                if (buffer.Count > 0)
                {
                    T item = buffer[0];
                    buffer.RemoveAt(0);

                    locker.Unlock();

                    consume(item);
                }
                else
                {
                    locker.Unlock();
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
