using System.Runtime.CompilerServices;

namespace Task4;

public class ThreadPool : IDisposable
{
    public int NumberOfThreads { get; private set; }

    private Thread[] threads;
    private Queue<Action> tasks;
    private volatile bool isDisposed;

    object dequeueSync = new();

    public ThreadPool(int numberOfThreads)
    {
        if (numberOfThreads <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfThreads), "Number of threads must be greater than 0.");
        }

        NumberOfThreads = numberOfThreads;
        threads = new Thread[NumberOfThreads];
        tasks = new Queue<Action>();
        isDisposed = false;

        InitializeThreads();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Enqueue(Action a)
    {
        if (isDisposed)
        {
            throw new InvalidOperationException("ThreadPool is disposed.");
        }

        tasks.Enqueue(a);

        lock(dequeueSync)
        {
            Monitor.Pulse(dequeueSync);
        }
    }

    public void Dispose()
    {
        isDisposed = true;

        lock(dequeueSync)
        {
            Monitor.PulseAll(dequeueSync);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        isDisposed = false;
    }

    private void InitializeThreads()
    {
        for (int i = 0; i < NumberOfThreads; i++)
        {
            threads[i] = new Thread(PerformTasks);
            threads[i].Start();
        }
    }

    private void PerformTasks()
    {
        while (true)
        {
            lock(dequeueSync)
            {
                while (tasks.Count == 0 && !isDisposed)
                {
                    Monitor.Wait(dequeueSync);
                }

                if (isDisposed)
                {
                    return;
                }

                tasks.Dequeue()();
            }
        }
    }
}
