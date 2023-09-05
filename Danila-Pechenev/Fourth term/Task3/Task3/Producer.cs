namespace Task3;

public class Producer : Participant
{
    public Producer(Semaphore semaphore, int pauseBetweenActions, List<int> numbers)
        : base(semaphore, pauseBetweenActions, numbers)
    {
    }

    protected override void Act()
    {
        while (!stop)
        {
            semaphore.WaitOne();

            int n = random.Next(1000000);
            numbers.Add(n);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} adds {n}.");

            semaphore.Release();

            Thread.Sleep(pauseBetweenActions);
        }
    }
}
