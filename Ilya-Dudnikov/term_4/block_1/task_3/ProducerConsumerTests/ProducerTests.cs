using ProducerConsumer.Producer;

namespace ProducerConsumerTests;

public class ProducerTests
{
    private volatile List<int> buffer;
    private volatile List<int> producedItems;
    private volatile Random random;

    [SetUp]
    public void Setup()
    {
        buffer = new List<int>();
        producedItems = new List<int>();
        random = new Random();
    }

    [Test]
    public void OneProducer()
    {
        var producer = new Producer<int>(new Mutex(), buffer, () =>
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
        var mutex = new Mutex();
        var producers = Enumerable.Range(0, 5).Select(
            _ => new Producer<int>(
                mutex,
                buffer,
                () =>
                {
                    var item = random.Next();
                    producedItems.Add(item);
                    return item;
                })).ToList();

        var threads = producers.Select(producer => new Thread(producer.Run)).ToList();
        threads.ForEach(thread => thread.Start());
        Thread.Sleep(100);

        producers.ForEach(producer => producer.Stop());
        threads.ForEach(thread => thread.Join());

        Assert.That(buffer, Is.EqualTo(producedItems));
    }

    [Test]
    public void TenProducers()
    {
        var mutex = new Mutex();
        var producers = Enumerable.Range(0, 10).Select(
            _ => new Producer<int>(
                mutex,
                buffer,
                () =>
                {
                    var item = random.Next();
                    producedItems.Add(item);
                    return item;
                })).ToList();

        var threads = producers.Select(producer => new Thread(producer.Run)).ToList();
        threads.ForEach(thread => thread.Start());
        Thread.Sleep(100);

        producers.ForEach(producer => producer.Stop());
        threads.ForEach(thread => thread.Join());

        Assert.That(buffer, Is.EqualTo(producedItems));
    }
}