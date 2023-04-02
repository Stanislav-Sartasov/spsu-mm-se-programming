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

            numbers.Add(random.Next(1000000));

            semaphore.Release();

            Thread.Sleep(pauseBetweenActions);
        }
    }
}
