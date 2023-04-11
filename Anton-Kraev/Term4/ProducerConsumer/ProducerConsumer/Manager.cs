namespace ProducerConsumer;

public class Manager<T>: IDisposable
{
    private Mutex mutex = new();
    private Buffer<T> buffer = new();
    private List<Producer<T>> producers;
    private List<Consumer<T>> consumers;
    private List<Thread> threads;

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
        threads.ForEach(thread => thread.Start());
    }

    public void Stop()
    {
        producers.ForEach(producer => producer.Stop());
        consumers.ForEach(consumer => consumer.Stop());
        threads.ForEach(thread => thread.Join());
    }

    public void Dispose()
    {
        producers.Clear();
        consumers.Clear();
        threads.Clear();
        mutex.Dispose();
    }
}