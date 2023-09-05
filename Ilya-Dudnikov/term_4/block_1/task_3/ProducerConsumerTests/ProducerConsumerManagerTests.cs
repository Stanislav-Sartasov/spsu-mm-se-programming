using ProducerConsumer;

namespace ProducerConsumerTests;

public class ProducerConsumerManagerTests
{
    private const int SleepInterval = 100;

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void OneConsumerOneProducers()
    {
        var manager = new ProducerConsumerManager(1, 1);
        manager.Run();
        Thread.Sleep(SleepInterval);
        manager.Stop();
    }

    [Test]
    public void OneConsumerMultipleProducers()
    {
        var manager = new ProducerConsumerManager(1, 5);
        manager.Run();
        Thread.Sleep(SleepInterval);
        manager.Stop();
    }

    [Test]
    public void MultipleConsumersOneProducer()
    {
        var manager = new ProducerConsumerManager(5, 1);
        manager.Run();
        Thread.Sleep(SleepInterval);
        manager.Stop();
    }

    [Test]
    public void MultipleConsumersMultipleProducers()
    {
        var manager = new ProducerConsumerManager(5, 5);
        manager.Run();
        Thread.Sleep(SleepInterval);
        manager.Stop();
    }
}