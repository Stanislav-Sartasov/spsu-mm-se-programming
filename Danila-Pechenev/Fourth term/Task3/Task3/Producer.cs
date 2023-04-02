namespace Task3;

public class Producer : Participant
{
    public Producer(Semaphore semaphore, int maxObjectsNumber, int pauseBetweenActions, List<int> numbers)
        : base(semaphore, maxObjectsNumber, pauseBetweenActions, numbers)
    {
    }

    protected override void Act()
    {
        int n = random.Next(1, maxObjectsNumber + 1);

        semaphore.WaitOne();

        for (int i = 0; i < n; i++)
        {
            numbers.Add(random.Next(1000000));
            Thread.Sleep(pauseBetweenActions);
        }

        semaphore.Release();
    }
}
