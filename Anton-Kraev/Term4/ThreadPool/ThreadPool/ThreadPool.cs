namespace ThreadPool;

public class ThreadPool : IDisposable
{
    private const int ThreadCount = 4;

    private List<Thread> threads;
    private Queue<Action> queue = new();

    private volatile bool isDisposed;

    public ThreadPool()
    {
        threads = Enumerable.Range(0, ThreadCount)
            .Select(_ => new Thread(ThreadMethod))
            .ToList();

        threads.ForEach(thread => thread.Start());
    }

    public void Enqueue(Action action)
    {
        if (isDisposed)
        {
            throw new InvalidOperationException(
            "Error adding a new task to the queue, because the thread pool was destroyed"
            );
        }

        lock (queue)
        {
            queue.Enqueue(action);
            Monitor.Pulse(queue);
        }
    }
    
    private void ThreadMethod()
    {
        while (true)
        {
            Action? task;

            lock (queue)
            {
                while (!queue.TryDequeue(out task))
                {
                    if (isDisposed) return;
                    Monitor.Wait(queue);
                }
            }

            task?.Invoke();
        }
    }

    public void Dispose()
    {
        if (isDisposed) return;
        isDisposed = true;

        lock (queue)
        {
            Monitor.PulseAll(queue);
        }
        threads.ForEach(th => th.Join());

        threads.Clear();
        queue.Clear();
    }
}