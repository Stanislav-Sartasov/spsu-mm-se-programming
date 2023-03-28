using ProducerConsumer;

namespace ProducerConsumerTests;

public class ProducerTests
{
    [Test]
    public void StartTest()
    {
        var mutex = new Mutex();
        var applications = Enumerable.Range(0, 10)
            .Select(x => new Application(x * x + 50, CoverType.Laminate, DateTime.Now.AddDays(x)))
            .ToList();

        var producers = Enumerable.Range(0, 5).Select(x => new Producer(mutex, applications)).ToList();
        producers.ForEach(x => x.Start());
        producers.ForEach(x => x.Stop());
        mutex.Dispose();
        Assert.That(producers.Count, Is.EqualTo(5));
        Assert.Zero(applications.Count);
    }
}