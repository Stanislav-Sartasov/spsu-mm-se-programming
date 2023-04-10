namespace ThreadPool;

public class ThreadPool : IDisposable
{
    public int Capacity { get; init; }
    private object actionSynchronizer = new();
    private object disposeSynchronizer = new();
    private List<Thread> threads = new();
    private Queue<Action> availableActions = new();
    private volatile bool isDisposed;
    private volatile int createdThreads;
    private volatile int freeThreads;

    public ThreadPool(int capacity)
    {
        Capacity = capacity;
    }

    public void Enqueue(Action a)
    {
        Monitor.Enter(disposeSynchronizer);
        try
        {
            if (isDisposed)
                throw new InvalidOperationException("Thread pool object was disposed");
        }
        finally
        {
            Monitor.Exit(disposeSynchronizer);
        }

        Monitor.Enter(actionSynchronizer);
        availableActions.Enqueue(a);
    
        // Threads are initialized as needed
        if (freeThreads == 0 && createdThreads != Capacity)
        {
            createdThreads++;
            var thread = new Thread(RunThread);
            threads.Add(thread);
            thread.Start();
        }

        Monitor.Pulse(actionSynchronizer); // There is no need to awaken more than one thread
        Monitor.Exit(actionSynchronizer);
    }

    public void Dispose()
    {
        lock (disposeSynchronizer)
        {
            isDisposed = true; // Restriction on adding new actions to be performed
        }

        // Notify threads that there are no more expected tasks
        Monitor.Enter(actionSynchronizer);
        Monitor.PulseAll(actionSynchronizer);
        Monitor.Exit(actionSynchronizer);

        // Waits until all added tasks are completed
        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    private void RunThread()
    {
        while (true)
        {
            Monitor.Enter(actionSynchronizer);

            while (!availableActions.Any())
            {
                Monitor.Enter(disposeSynchronizer);
                var toExit = isDisposed;
                Monitor.Exit(disposeSynchronizer);

                if (toExit)
                {
                    Monitor.Exit(actionSynchronizer);
                    return;
                }

                freeThreads++;
                Monitor.Wait(actionSynchronizer);
                freeThreads--;
            }

            var action = availableActions.Dequeue();
            Monitor.Exit(actionSynchronizer);
            action();
        }
    }
}