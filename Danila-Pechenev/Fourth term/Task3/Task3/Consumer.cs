namespace Task3;

public class Consumer : Participant
{
    public Consumer(Semaphore semaphore, int maxObjectsNumber, int pauseBetweenActions, List<int> numbers)
        : base(semaphore, maxObjectsNumber, pauseBetweenActions, numbers)
    {
    }

    protected override void Act()
    {
        int n = random.Next(1, maxObjectsNumber + 1);

        semaphore.WaitOne();

        n = Math.Min(n, numbers.Count);
        for (int i = 0; i < n; i++)
        {
            numbers.RemoveAt(0);
            Thread.Sleep(pauseBetweenActions);
        }

        semaphore.Release();
    }
}
