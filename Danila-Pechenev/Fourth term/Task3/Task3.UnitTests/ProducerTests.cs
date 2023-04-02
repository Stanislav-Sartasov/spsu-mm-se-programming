using Task3;

namespace Task3.UnitTests;

public class Tests
{
    [Test]
    public void AddElementTest()
    {
        var semaphore = new Semaphore(1, 1);
        var numbers = new List<int>();
        var producer = new Producer(semaphore, 3000, numbers);
        producer.StartAction();
        Thread.Sleep(300);
        producer.StopAction();
        Assert.AreEqual(1, numbers.Count);
    }
}