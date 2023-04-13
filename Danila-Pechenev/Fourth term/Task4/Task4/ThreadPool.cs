using System.Runtime.CompilerServices;

namespace Task4;

public class ThreadPool : IDisposable
{
    public int NumberOfThreads { get; private set; }

    private Thread[] threads;
    private Queue<Action> tasks;
    bool isInitialized;  // Not volatile, because only the main thread can change this field
    bool isDisposed;  // Not volatile, because only the main thread can change this field

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
        isInitialized = false;
        isDisposed = false;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Enqueue(Action a)
    {
        if (!isInitialized)
        {
            InitializeThreads();
        }

        tasks.Enqueue(a);
        Monitor.Pulse(dequeueSync);
    }

    public void Dispose()
    {
        isDisposed = true;
        Monitor.PulseAll(dequeueSync);

        foreach (var thread in threads)
        {
            thread.Join();
        }

        isDisposed = false;
        isInitialized = false;
    }

    private void InitializeThreads()
    {
        for (int i = 0; i < NumberOfThreads; i++)
        {
            threads[i] = new Thread(PerformTasks);
            threads[i].Start();
        }

        isInitialized = true;
    }

    private void PerformTasks()
    {
        while (true)
        {
            lock(dequeueSync)
            {
                if (isDisposed)
                {
                    return;
                }

                if (tasks.Count == 0)
                {
                    Monitor.Wait(dequeueSync);
                }

                tasks.Dequeue()();
            }
        }
    }
}
