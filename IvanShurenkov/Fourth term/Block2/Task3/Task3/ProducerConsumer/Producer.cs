using Task3.Locks;

namespace Task3.ProducerConsumer
{
    public class Producer<T>
    {
        private List<T> buffer;
        private volatile bool stop = true;
        private Func<T> produce;
        private int timeout = 10;
        private Thread thread;
        private ILock locker;

        public Producer(List<T> buffer, ILock locker, Func<T> produce)
        {
            this.buffer = buffer;
            this.produce = produce;
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
                T item = produce();

                locker.Lock();

                buffer.Add(item);

                locker.Unlock();

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
