using ProducerConsumer.Consumer;
using ProducerConsumer.Producer;

namespace ProducerConsumerTests;

public class ProducerConsumerTests
{
    private const int SleepInterval = 100;
    private volatile List<int> buffer;
    private volatile List<int> consumedItems;

    private Mutex mutex;
    private volatile List<int> producedItems;
    private Random random;

    [SetUp]
    public void Setup()
    {
        buffer = new List<int>();
        consumedItems = new List<int>();
        producedItems = new List<int>();
        random = new Random();
        mutex = new Mutex();
    }

    [Test]
    public void OneConsumerOneProducer()
    {
        var producer = new Producer<int>(mutex, buffer, () =>
        {
            var item = random.Next();
            producedItems.Add(item);
            return item;
        });
        var producerThread = new Thread(producer.Run);

        var consumer = new Consumer<int>(mutex, buffer, item =>
        {
            mutex.WaitOne();
            consumedItems.Add(item);
            mutex.ReleaseMutex();
        });
        var consumerThread = new Thread(consumer.Run);

        producerThread.Start();
        consumerThread.Start();

        Thread.Sleep(SleepInterval);

        producer.Stop();
        producerThread.Join();

        while (buffer.Any())
            Thread.Sleep(SleepInterval);

        consumer.Stop();
        consumerThread.Join();

        Assert.That(consumedItems, Is.EqualTo(producedItems));
    }

    [Test]
    public void OneConsumerMultipleProducers()
    {
        var consumer = new Consumer<int>(mutex, buffer, item =>
        {
            mutex.WaitOne();
            consumedItems.Add(item);
            mutex.ReleaseMutex();
        });
        var consumerThread = new Thread(consumer.Run);

        var producers = Enumerable.Range(0, 5).Select(
            _ => new Producer<int>(
                mutex,
                buffer,
                () =>
                {
                    var item = random.Next();

                    mutex.WaitOne();
                    producedItems.Add(item);
                    mutex.ReleaseMutex();

                    return item;
                })).ToList();
        var threads = producers.Select(producer => new Thread(producer.Run)).ToList();

        consumerThread.Start();
        threads.ForEach(thread => thread.Start());

        Thread.Sleep(SleepInterval);

        producers.ForEach(producer => producer.Stop());
        threads.ForEach(thread => thread.Join());

        // Wait until consumer consumes everything that was produced
        while (buffer.Any())
            Thread.Sleep(SleepInterval);

        consumer.Stop();
        consumerThread.Join();

        consumedItems.Sort();
        producedItems.Sort();

        Assert.That(consumedItems, Is.EqualTo(producedItems));
    }

    [Test]
    public void MultipleConsumersOneProducer()
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

        var producer = new Producer<int>(mutex, buffer, () =>
        {
            var item = random.Next();
            producedItems.Add(item);
            return item;
        });
        var producerThread = new Thread(producer.Run);

        threads.ForEach(thread => thread.Start());
        producerThread.Start();

        Thread.Sleep(SleepInterval);

        producer.Stop();
        producerThread.Join();

        while (buffer.Any())
            Thread.Sleep(SleepInterval);

        consumers.ForEach(consumer => consumer.Stop());
        threads.ForEach(thread => thread.Join());

        consumedItems.Sort();
        producedItems.Sort();

        Assert.That(consumedItems, Is.EqualTo(producedItems));
    }

    [Test]
    public void MultipleConsumersMultipleProducers()
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
        var consumerThreads = consumers.Select(consumer => new Thread(consumer.Run)).ToList();

        var producers = Enumerable.Range(0, 5).Select(
            _ => new Producer<int>(
                mutex,
                buffer,
                () =>
                {
                    var item = random.Next();

                    mutex.WaitOne();
                    producedItems.Add(item);
                    mutex.ReleaseMutex();

                    return item;
                })).ToList();
        var producerThreads = producers.Select(producer => new Thread(producer.Run)).ToList();

        consumerThreads.ForEach(thread => thread.Start());
        producerThreads.ForEach(thread => thread.Start());

        Thread.Sleep(SleepInterval);

        producers.ForEach(producer => producer.Stop());
        producerThreads.ForEach(thread => thread.Join());

        while (buffer.Any())
            Thread.Sleep(SleepInterval);

        consumers.ForEach(consumer => consumer.Stop());
        consumerThreads.ForEach(thread => thread.Join());

        consumedItems.Sort();
        producedItems.Sort();
        Assert.That(consumedItems, Is.EqualTo(producedItems));
    }
}