using System.Runtime.CompilerServices;

namespace ThreadPool
{
    public class ThreadPool : IDisposable
    {
        volatile bool isStop = false;

        List<Thread> threads;
        Queue<(Action<object?> Task, object? Param)> tasks;

        object isRunningNotification = new object();

        public ThreadPool(int maxThreadsCount)
        {
            if (maxThreadsCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxThreadsCount), maxThreadsCount, "The number of threads in the pool must be >= 1");

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

            lock(isRunningNotification)
            {
                tasks.Enqueue((task, param));
                Monitor.PulseAll(isRunningNotification);
            }
        }

        private void ThreadWorking()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} was started...");

            while (!isStop)
            {
                Action<object?> task = null;
                object? param = null;

                lock (isRunningNotification)
                {
                    while (tasks.Count == 0 && !isStop) // Wait until the tasks appear in queue
                        Monitor.Wait(isRunningNotification);

                    if (isStop)
                        break;

                    if (tasks.Count > 0)
                        (task, param) = tasks.Dequeue();
                }

                try
                {
                    if (task != null)
                        task(param); // Execute task
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error {e} was accured in {Thread.CurrentThread.ManagedThreadId} thread!!!");
                }
            }

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} was stopped.");
        }

        public void Dispose()
        {
            Volatile.Write(ref isStop, true);

            lock(isRunningNotification)
                Monitor.PulseAll(isRunningNotification);

            threads.ForEach(x => x.Join());
            tasks.Clear();

            Console.WriteLine("ThreadPool was destroyed!");
        }
    }
}