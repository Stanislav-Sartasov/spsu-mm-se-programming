using ProducerConsumer;

namespace ProducerConsumerTests;

public class ConsumerTests
{
    [Test]
    public void StartTest()
    {
        var mutex = new Mutex();
        var applications = new List<Application>();
        var consumers = Enumerable.Range(0, 5).Select(x => new Consumer(mutex, applications)).ToList();
        consumers.ForEach(x => x.Start());
        Thread.Sleep(2000);
        consumers.ForEach(x => x.Stop());
        mutex.Dispose();
        Assert.That(consumers.Count, Is.EqualTo(5));
        Assert.Positive(applications.Count);
    }
}