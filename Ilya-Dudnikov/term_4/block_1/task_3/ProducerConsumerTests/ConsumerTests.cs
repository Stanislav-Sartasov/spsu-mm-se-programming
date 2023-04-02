using ProducerConsumer.Consumer;

namespace ProducerConsumerTests;

public class ConsumerTests
{
    private const int SleepInterval = 100;
    private const int NumberOfConsumers = 1000;
    private volatile List<int> buffer;
    private volatile List<int> consumedItems;
    private Mutex mutex;
    private volatile List<int> originalBuffer;
    private Random random;

    [SetUp]
    public void Setup()
    {
        random = new Random();
        mutex = new Mutex();

        consumedItems = new List<int>();
        buffer = Enumerable.Range(0, NumberOfConsumers).Select(_ => random.Next()).ToList();

        originalBuffer = buffer.Select(x => x).ToList();
    }

    [Test]
    public void OneConsumer()
    {
        var consumer = new Consumer<int>(mutex, buffer, consumedItems.Add);
        var consumerThread = new Thread(consumer.Run);
        consumerThread.Start();
        Thread.Sleep(SleepInterval);
        consumer.Stop();
        consumerThread.Join();

        Assert.That(consumedItems, Is.EqualTo(originalBuffer));
    }

    [Test]
    public void FiveConsumers()
    {
        var consumers = Enumerable.Range(0, 5).Select(
            _ => new Consumer<int>(
                mutex,
                buffer,
                item =>
                {
                    mutex.WaitOne();
                    consumedItems.Add(item);
                    mutex.ReleaseMutex();
                })).ToList();

        var threads = consumers.Select(consumer => new Thread(consumer.Run)).ToList();
        threads.ForEach(thread => thread.Start());
        Thread.Sleep(SleepInterval);

        consumers.ForEach(consumer => consumer.Stop());
        threads.ForEach(thread => thread.Join());

        consumedItems.Sort();
        originalBuffer.Sort();
        Assert.That(consumedItems, Is.EqualTo(originalBuffer));
    }

    [Test]
    public void TenConsumers()
    {
        var consumers = Enumerable.Range(0, 10).Select(
            _ => new Consumer<int>(
                mutex,
                buffer,
                item =>
                {
                    mutex.WaitOne();
                    consumedItems.Add(item);
                    mutex.ReleaseMutex();
                })).ToList();

        var threads = consumers.Select(consumer => new Thread(consumer.Run)).ToList();
        threads.ForEach(thread => thread.Start());
        Thread.Sleep(SleepInterval);

        consumers.ForEach(consumer => consumer.Stop());
        threads.ForEach(thread => thread.Join());

        consumedItems.Sort();
        originalBuffer.Sort();
        Assert.That(consumedItems, Is.EqualTo(originalBuffer));
    }
}