using System.Diagnostics;

namespace Task3.UnitTests;

public class FactoryTests
{
    [Test]
    public void RemoveElementTest()
    {
        Stopwatch stopwatch = new Stopwatch();
        var factory = new Factory(6, 6, 10);

        factory.Start();
        Thread.Sleep(3000);
        stopwatch.Start();
        factory.Stop();
        stopwatch.Stop();
        Assert.Less(stopwatch.ElapsedMilliseconds, 500);
    }
}