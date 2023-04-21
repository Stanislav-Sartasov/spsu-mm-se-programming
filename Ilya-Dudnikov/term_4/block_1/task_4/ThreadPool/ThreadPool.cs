using System.Runtime.CompilerServices;

namespace ThreadPool;

public class ThreadPool : IDisposable
{
    private Queue<Action> taskQueue;
    private int threadCount;
    private List<Thread> threadList;
    private volatile bool isStopped;
    private volatile bool isDisposed;

    private object sync;

    public ThreadPool(int threadCount)
    {
        this.threadCount = threadCount;
        taskQueue = new Queue<Action>();
        threadList = new List<Thread>();
        sync = new object();
    }

    public void Enqueue(Action action)
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);

        lock (sync)
        {
            taskQueue.Enqueue(action);
            Monitor.PulseAll(sync);
        }
    }

    public void Start()
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);

        isStopped = false;
        for (var i = 0; i < threadCount; i++)
        {
            threadList.Add(new Thread(RunAction));
            threadList.Last().Start();
        }
    }

    private void Stop()
    {
        ObjectDisposedException.ThrowIf(isDisposed, this);

        isStopped = true;
        lock (sync)
        {
            Monitor.PulseAll(sync);
        }

        threadList.ForEach(thread => thread.Join());
    }

    private void RunAction()
    {
        while (true)
        {
            var action = () => { };
            lock (sync)
            {
                while (!taskQueue.Any() && !isStopped) Monitor.Wait(sync);

                if (isStopped) return;

                action = taskQueue.Dequeue();
            }

            action();
        }
    }

    public void Dispose()
    {
        Stop();
        isDisposed = true;
        taskQueue.Clear();
        threadList.Clear();
    }
}