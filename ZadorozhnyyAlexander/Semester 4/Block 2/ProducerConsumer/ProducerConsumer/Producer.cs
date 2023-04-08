namespace ProducerConsumer
{
    public class Producer<T>
    {
        List<T> applications;
        Func<T> produceFunc;

        Thread thread;
        int pauseTime;

        Semaphore semaphore;
        volatile bool stop;

        public Producer(Semaphore semaphore, int pauseTime, List<T> applications, Func<T> produceFunc)
        {
            thread = new Thread(AddApplication);
            this.semaphore = semaphore;
            this.pauseTime = pauseTime;
            this.applications = applications;
            this.produceFunc = produceFunc;
        }

        private void AddApplication()
        {
            while (!stop)
            {
                semaphore.WaitOne();

                T application = produceFunc();
                applications.Add(application);

                Console.WriteLine($"Application {application} was added by {Thread.CurrentThread.ManagedThreadId} thread!");

                semaphore.Release();

                Thread.Sleep(pauseTime);
            }
        }

        public void Start()
        {
            Volatile.Write(ref stop, false);
            thread.Start();
        }

        public void Stop()
        {
            Volatile.Write(ref stop, true);
            thread.Join();
        }
    }

}
