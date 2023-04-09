namespace ThreadPool
{
    public class ThreadPool : IDisposable
    {
        volatile bool isStop = false;

        List<Thread> threads;
        Queue<(Action<object?> Task, object? Param)> tasks;

        AutoResetEvent isRunningNotification;
        Semaphore enqueueSemaphore;

        public ThreadPool(int maxThreadsCount)
        {
            if (maxThreadsCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxThreadsCount), maxThreadsCount, "The number of threads in the pool must be >= 1");

            isRunningNotification = new(true);
            enqueueSemaphore = new Semaphore(1, 1);

            tasks = new();
            threads = (from _ in Enumerable.Range(0, maxThreadsCount) select CreateThread()).ToList();

            Console.WriteLine("ThreadPool was created!");
        }

        private Thread CreateThread()
        {
            var thread = new Thread(ThreadWorking);
            thread.Start();
            return thread;
        }

        public void EnqueueTask(Action task) => EnqueueTask(null, _ => task());

        public void EnqueueTask(object? param, Action<object?> task)
        {
            if (isStop)
                throw new InvalidOperationException("Thread pool already destroyed!");

            // Blocking Queue realization by simple Queue and individual semaphore
            enqueueSemaphore.WaitOne();

            if (isStop)
                throw new InvalidOperationException("Thread pool already destroyed!");
            tasks.Enqueue((task, param));

            enqueueSemaphore.Release();

            isRunningNotification.Set();
        }

        private void ThreadWorking()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} was started...");

            while (!isStop)
            {
                isRunningNotification.WaitOne();
                if (isStop) break; // Check if ThreadPool was stopped while we were waiting

                enqueueSemaphore.WaitOne(); // Requesting access to queues

                while (tasks.Count == 0) // Wait until the tasks appear in queue
                {
                    enqueueSemaphore.Release(); // Release the queue for another thread
                    isRunningNotification.WaitOne(); // Waiting for execution
                    if (isStop) break;

                    enqueueSemaphore.WaitOne(); // Requesting access to queues
                }

                var (task, param) = tasks.Dequeue();

                if (tasks.Count > 0) // Check if exist another task in queue and start it
                    isRunningNotification.Set();

                enqueueSemaphore.Release(); // Allow another thread use queue

                try
                {
                    task(param); // Execute task
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e} was accured in {Thread.CurrentThread.ManagedThreadId} thread!!!");
                }
            }

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} was stopped.");
            isRunningNotification.Set();
        }

        public void Dispose()
        {
            Volatile.Write(ref isStop, true);

            isRunningNotification.Set();
            foreach (var thread in threads)
                thread.Join();

            enqueueSemaphore.Dispose();
            isRunningNotification.Dispose();
            Console.WriteLine("ThreadPool was destroyed!");
        }
    }
}