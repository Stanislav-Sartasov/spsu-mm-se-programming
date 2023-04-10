namespace ThreadPool.Test;

public class ThreadPoolTests
{
    private ThreadPool threadpool;
    private volatile int taskCount;

    [SetUp]
    public void SetUp()
    {
        taskCount = 0;
    }

    [Test]
    public void CountTest()
    {
        var threadCapacity = Environment.ProcessorCount / 2;
        var taskCapacity = 10 * threadCapacity;
        threadpool = new ThreadPool(threadCapacity);

        for (var i = 0; i < taskCapacity; i++)
            threadpool.Enqueue(Count);

        threadpool.Dispose();

        Assert.That(taskCount, Is.EqualTo(taskCapacity));
        Assert.That(threadpool.ThreadCount, Is.Zero);
    }

    [Test]
    public void LongCountTest()
    {
        var threadCapacity = Environment.ProcessorCount / 2;
        var taskCapacity = 50 * threadCapacity;
        threadpool = new ThreadPool(threadCapacity);

        for (var i = 0; i < taskCapacity; i++)
            threadpool.Enqueue(Count);

        threadpool.Dispose();

        Assert.That(taskCount, Is.EqualTo(taskCapacity));
        Assert.That(threadpool.ThreadCount, Is.Zero);
    }

    [Test]
    public void MoreThreadsThanAreAvailableTest()
    {
        var threadCapacity = Environment.ProcessorCount * 2;
        var taskCapacity = 5 * threadCapacity;
        threadpool = new ThreadPool(threadCapacity);

        for (var i = 0; i < taskCapacity; i++)
            threadpool.Enqueue(Count);

        threadpool.Dispose();

        Assert.That(taskCount, Is.EqualTo(taskCapacity));
        Assert.That(threadpool.ThreadCount, Is.Zero);
    }

    [Test]
    public void MoreThreadsThanTasksTest()
    {
        var threadCapacity = Environment.ProcessorCount - 2;
        var taskCapacity = threadCapacity / 2;
        threadpool = new ThreadPool(threadCapacity);

        for (var i = 0; i < taskCapacity; i++)
            threadpool.Enqueue(Count);

        threadpool.Dispose();

        Assert.That(taskCount, Is.EqualTo(taskCapacity));
        Assert.That(threadpool.ThreadCount, Is.Zero);
    }
    
    [Test]
    public void CountTwiceTest()
    {
        var threadCapacity = Environment.ProcessorCount / 2;
        var taskCapacity = threadCapacity * 5;
        threadpool = new ThreadPool(threadCapacity);

        for (var i = 0; i < taskCapacity; i++)
            threadpool.Enqueue(Count);
        
        Thread.Sleep(2000);
        
        for (var i = 0; i < taskCapacity; i++)
            threadpool.Enqueue(Count);

        threadpool.Dispose();

        Assert.That(taskCount, Is.EqualTo(2 * taskCapacity));
        Assert.That(threadpool.ThreadCount, Is.Zero);
    }

    [Test]
    public void EnqueueAfterDispose()
    {
        var threadCapacity = 1;
        threadpool = new ThreadPool(threadCapacity);

        threadpool.Enqueue(Count);
        threadpool.Dispose();

        Assert.Throws<InvalidOperationException>(() => threadpool.Enqueue(Count));
    }

    private void Count()
    {
        var sum = 0;

        for (var i = 1; i < 1000000; i++)
            sum += i * i;

        Interlocked.Increment(ref taskCount);
    }
}