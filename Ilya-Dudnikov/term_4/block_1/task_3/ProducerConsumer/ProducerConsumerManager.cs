using ProducerConsumer.Consumer;
using ProducerConsumer.Producer;

namespace ProducerConsumer;

public class ProducerConsumerManager : IDisposable
{
    private const int WorkEmulationTime = 100;
    private readonly List<IConsumer> consumers;
    private readonly Mutex mutex;
    private readonly List<IProducer> producers;
    private readonly List<Thread> threadList;
    private volatile List<int> buffer;
    private volatile Random random;

    public ProducerConsumerManager(int numberOfConsumers, int numberOfProducers)
    {
        mutex = new Mutex();
        buffer = new List<int>();
        random = new Random();
        consumers = Enumerable.Range(0, numberOfConsumers).Select(
            _ => new Consumer<int>(
                mutex,
                buffer,
                _ =>
                {
                    // Work emulation
                    Thread.Sleep(WorkEmulationTime);
                })).ToList<IConsumer>();
        producers = Enumerable.Range(0, numberOfProducers).Select(
            _ => new Producer<int>(mutex, buffer, random.Next)
        ).ToList<IProducer>();

        threadList = consumers.Select(consumer => new Thread(consumer.Run))
            .Concat(producers.Select(producer => new Thread(producer.Run)))
            .ToList();
    }

    public void Dispose()
    {
        consumers.Clear();
        producers.Clear();
        threadList.Clear();
        mutex.Dispose();
    }

    public void Run()
    {
        threadList.ForEach(thread => thread.Start());
    }

    public void Stop()
    {
        producers.ForEach(producer => producer.Stop());
        consumers.ForEach(consumer => consumer.Stop());
        threadList.ForEach(thread => thread.Join());
        Dispose();
    }
}