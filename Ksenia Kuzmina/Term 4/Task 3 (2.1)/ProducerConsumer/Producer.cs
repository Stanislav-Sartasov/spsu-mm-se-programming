namespace ProducerConsumer
{
    public class Producer
    {
        private Semaphore producerSemaphore;
        private Semaphore consumerSemaphore;
        private List<Thread> threads = new List<Thread>();
        private List<int> buffer;
        private CancellationTokenSource cancellationTokenSource;
        private int num;

        public Producer(CancellationTokenSource cts, List<int> buff, Semaphore ps, Semaphore cs, int n) 
        {
            cancellationTokenSource = cts;
            buffer = buff;
            producerSemaphore = ps;
            consumerSemaphore = cs;
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
                Thread producerThread = new Thread(() => ProducerThreadFunction(cancellationTokenSource.Token));
                threads.Add(producerThread);
                producerThread.Start();
                Thread.Sleep(1000);
            }
        }

        private void ProducerThreadFunction(CancellationToken cancellationToken)
        {
            Random random = new Random();
            while (!cancellationToken.IsCancellationRequested)
            {
                int item = random.Next(100);
                if (producerSemaphore.WaitOne(0))
                {
                    lock (buffer)
                    {
                        buffer.Add(item);
                    }

                    // The output is a reference to an article about semaphores on metanit
                    Console.WriteLine($"Reader {Thread.CurrentThread.ManagedThreadId} chose the place {item}");

                    consumerSemaphore.Release();
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
