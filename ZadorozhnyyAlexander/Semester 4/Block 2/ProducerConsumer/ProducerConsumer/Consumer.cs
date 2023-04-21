namespace ProducerConsumer
{
    public class Consumer<T>
    {
        List<T> applications;

        Thread thread;
        int pauseTime;

        Semaphore semaphore;
        volatile bool stop;


        public Consumer(Semaphore semaphore, int pauseTime, List<T> applications)
        {
            thread = new Thread(TakeApplication);
            this.semaphore = semaphore;
            this.pauseTime = pauseTime;
            this.applications = applications;
        }

        private void TakeApplication()
        {
            while (!stop)
            {
                semaphore.WaitOne();

                if (!(applications.Count == 0))
                {
                    Console.WriteLine($"Application {applications[0]} was removed by {Thread.CurrentThread.ManagedThreadId} thread!");
                    applications.RemoveAt(0);
                }

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
