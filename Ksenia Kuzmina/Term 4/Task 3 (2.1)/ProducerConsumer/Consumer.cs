namespace ProducerConsumer
{
    public class Consumer
    {
        private Semaphore consumerSemaphore;
        private Semaphore producerSemaphore;
        private List<Thread> threads = new List<Thread>();
        private List<int> buffer;
        private CancellationTokenSource cancellationTokenSource;
        private int num;

        public Consumer(Semaphore cs, Semaphore ps, List<int> buff, CancellationTokenSource cts, int n)
        {
            consumerSemaphore = cs;
            producerSemaphore = ps;
            buffer = buff;
            cancellationTokenSource = cts;
            num = n;
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        public void Start()
        {
            for (int i = 0; i < num; i++)
            {
                Thread consumerThread = new Thread(() => ConsumerThreadFunction(cancellationTokenSource.Token));
                threads.Add(consumerThread);
                consumerThread.Start();
            }
        }

        private void ConsumerThreadFunction(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (consumerSemaphore.WaitOne(1))
                {
                    int item;
                    lock (buffer)
                    {
                        item = buffer[0];
                        buffer.RemoveAt(0);
                    }

                    // The output is a reference to an article about semaphores on metanit
                    Console.WriteLine($"Librarian {Thread.CurrentThread.ManagedThreadId} asked to free the place {item} "); 

                    producerSemaphore.Release();
                    Thread.Sleep(1000);
                }
            }
        }

    }
}
