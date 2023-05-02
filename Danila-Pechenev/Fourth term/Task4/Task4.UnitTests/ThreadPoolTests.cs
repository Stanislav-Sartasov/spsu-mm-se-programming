using Task4;

namespace Task4.UnitTests;

public class ThreadPoolTests
{
    private volatile int testVar = 0;

    [Test]
    public void ThreadPoolCreationTest()
    {
        var pool = new ThreadPool(5);
    }

    [Test]
    public void EnqueueTest()
    {
        var pool = new ThreadPool(8);
        pool.Enqueue(() => { testVar++; });
        Thread.Sleep(100);
        pool.Dispose();

        Assert.AreEqual(1, testVar);
    }

    [Test]
    public void IncorrectNumberOfThreadsTest()
    {
        try
        {
            var pool = new ThreadPool(-1);
            Assert.Fail();
        }
        catch
        {
            Assert.Pass();
        }
    }
}
