using ProducerConsumer.Producer;

namespace ProducerConsumerTests;

public class ProducerTests
{
    private const int SleepInterval = 100;
    private volatile List<int> buffer;
    private Mutex mutex;
    private volatile List<int> producedItems;
    private Random random;

    [SetUp]
    public void Setup()
    {
        buffer = new List<int>();
        producedItems = new List<int>();
        random = new Random();
        mutex = new Mutex();
    }

    [Test]
    public void OneProducer()
    {
        var producer = new Producer<int>(mutex, buffer, () =>
        {
            var item = random.Next();
            producedItems.Add(item);
            return item;
        });

        var producerThread = new Thread(producer.Run);
        producerThread.Start();
        Thread.Sleep(1);
        producer.Stop();
        producerThread.Join();

        Assert.That(buffer, Is.EqualTo(producedItems));
    }

    [Test]
    public void FiveProducers()
    {
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
        threads.ForEach(thread => thread.Start());
        Thread.Sleep(SleepInterval);

        producers.ForEach(producer => producer.Stop());
        threads.ForEach(thread => thread.Join());

        buffer.Sort();
        producedItems.Sort();
        Assert.That(buffer, Is.EqualTo(producedItems));
    }

    [Test]
    public void TenProducers()
    {
        var producers = Enumerable.Range(0, 10).Select(
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
        threads.ForEach(thread => thread.Start());
        Thread.Sleep(SleepInterval);

        producers.ForEach(producer => producer.Stop());
        threads.ForEach(thread => thread.Join());

        buffer.Sort();
        producedItems.Sort();
        Assert.That(buffer, Is.EqualTo(producedItems));
    }
}