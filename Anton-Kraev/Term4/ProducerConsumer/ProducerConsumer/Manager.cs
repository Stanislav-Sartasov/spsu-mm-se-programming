namespace ProducerConsumer;

public class Manager<T> : IDisposable
{
    private readonly Mutex mutex = new();
    private readonly Buffer<T> buffer = new();
    private readonly List<Producer<T>> producers;
    private readonly List<Consumer<T>> consumers;
    private readonly List<Thread> threads;

    private volatile bool disposed;
    private volatile bool running;

    public Manager(int producersCount, int consumersCount, Func<T> produceFunc, Action<T> consumeAction)
    {
        producers = Enumerable.Range(0, producersCount)
            .Select(_ => new Producer<T>(mutex, buffer, produceFunc))
            .ToList();

        consumers = Enumerable.Range(0, consumersCount)
            .Select(_ => new Consumer<T>(mutex, buffer, consumeAction))
            .ToList();

        threads = producers.Select(producer => new Thread(producer.Run))
            .Concat(consumers.Select(consumer => new Thread(consumer.Run)))
            .ToList();
    }

    public void Start()
    {
        if (disposed)
        {
            throw new InvalidOperationException(
                "Error starting threads, because the manager was destroyed"
            );
        }

        if (running) return;

        running = true;

        threads.ForEach(thread => thread.Start());
    }

    public void Dispose()
    {
        if (disposed) return;

        disposed = true;
        running = false;

        producers.ForEach(producer => producer.Stop());
        consumers.ForEach(consumer => consumer.Stop());
        threads.ForEach(thread => thread.Join());

        producers.Clear();
        consumers.Clear();
        threads.Clear();

        mutex.Dispose();
    }
}