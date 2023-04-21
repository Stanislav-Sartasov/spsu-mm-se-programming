namespace ThreadPoolTests;

public class ThreadPoolCorrect
{
    private ThreadPool.ThreadPool threadPool;
    private volatile int taskCount;
    private Random random;

    [SetUp]
    public void SetUp()
    {
        threadPool = new ThreadPool.ThreadPool(5);
        taskCount = 0;
        random = new Random();
    }

    [Test]
    public void EnqueueTasks()
    {
        var tasks = 100;
        var timeout = 100;

        for (var i = 0; i < tasks; i++) threadPool.Enqueue(() => Interlocked.Increment(ref taskCount));

        threadPool.Start();
        Thread.Sleep(timeout);
        threadPool.Dispose();
        Assert.That(taskCount, Is.EqualTo(tasks));
    }

    [Test]
    public void OneTaskRun()
    {
        var tasks = 1;
        var timeout = 100;

        for (var i = 0; i < tasks; i++) threadPool.Enqueue(() => Thread.Sleep(random.Next(1, 50)));

        threadPool.Start();
        Thread.Sleep(timeout);
        threadPool.Dispose();
    }

    [Test]
    public void TenTasksRun()
    {
        var tasks = 10;
        var timeout = 1000;

        for (var i = 0; i < tasks; i++) threadPool.Enqueue(() => Thread.Sleep(random.Next(1, 50)));

        threadPool.Start();
        Thread.Sleep(timeout);
        threadPool.Dispose();
    }

    [Test]
    public void HundredTasksRun()
    {
        var tasks = 1000;
        var timeout = 10000;

        for (var i = 0; i < tasks; i++) threadPool.Enqueue(() => Thread.Sleep(random.Next(1, 50)));

        threadPool.Start();
        Thread.Sleep(timeout);
        threadPool.Dispose();
    }
}