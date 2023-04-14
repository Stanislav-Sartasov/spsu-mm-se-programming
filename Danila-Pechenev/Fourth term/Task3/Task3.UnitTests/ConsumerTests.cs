namespace Task3.UnitTests;

public class ConsumerTests
{
    [Test]
    public void RemoveElementTest()
    {
        var semaphore = new Semaphore(1, 1);
        var numbers = new List<int>() { 12, 56, 31 };
        var consumer = new Consumer(semaphore, 3000, numbers);
        consumer.StartAction();
        Thread.Sleep(300);
        consumer.StopAction();
        Assert.AreEqual(new List<int>{ 56, 31}, numbers);
    }
}