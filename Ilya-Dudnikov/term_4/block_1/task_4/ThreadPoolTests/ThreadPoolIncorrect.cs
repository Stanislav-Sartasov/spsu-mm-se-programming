namespace ThreadPoolTests;

public class Tests
{
    private ThreadPool.ThreadPool threadPool;

    [SetUp]
    public void Setup()
    {
        threadPool = new ThreadPool.ThreadPool(1);
    }

    [Test]
    public void StartDisposed()
    {
        threadPool.Dispose();
        Assert.Throws<ObjectDisposedException>(threadPool.Start);
    }

    [Test]
    public void EnqueueDisposed()
    {
        threadPool.Dispose();
        Assert.Throws<ObjectDisposedException>(() => threadPool.Enqueue(() => { }));
    }
}