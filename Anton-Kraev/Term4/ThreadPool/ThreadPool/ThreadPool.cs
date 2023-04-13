namespace ThreadPool;

public class ThreadPool : IDisposable
{
    private const int ThreadCount = 4;

    private List<Thread> threads;
    private Queue<Action> queue = new();

    private volatile bool isEmpty = true;
    private volatile bool isDisposed = false;

    public ThreadPool()
    {
        threads = Enumerable.Range(0, ThreadCount)
            .Select(_ => new Thread(ThreadMethod))
            .ToList();

        threads.ForEach(thread => thread.Start());
    }

    public void Enqueue(Action action)
    {
        if (isDisposed) throw new InvalidOperationException(
            "Error adding a new task to the queue, because the thread pool was destroyed"
            );

        lock (queue)
        {
            queue.Enqueue(action);
            isEmpty = false;
        }
    }
    
    private void ThreadMethod()
    {
        while (true)
        {
            if (isEmpty) continue;

            Action? task = null;

            lock (queue)
            {
                if (queue.Any())
                {
                    task = queue.Dequeue();
                }
                isEmpty = !queue.Any();
            }

            task?.Invoke();

            if (isDisposed && isEmpty) return;
        }
    }

    public void Dispose()
    {
        if (isDisposed) return;

        isDisposed = true;

        threads.ForEach(th => th.Join());

        threads.Clear();
        queue.Clear();
    }
}