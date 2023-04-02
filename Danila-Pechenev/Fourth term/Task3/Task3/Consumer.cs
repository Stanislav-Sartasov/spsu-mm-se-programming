namespace Task3;

public class Consumer : Participant
{
    public Consumer(Semaphore semaphore, int pauseBetweenActions, List<int> numbers)
        : base(semaphore, pauseBetweenActions, numbers)
    {
    }

    protected override void Act()
    {
        while (!stop)
        {
            semaphore.WaitOne();

            if (numbers.Count != 0)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} removes {numbers[0]}.");
                numbers.RemoveAt(0);
            }
            
            semaphore.Release();

            Thread.Sleep(pauseBetweenActions);
        }
    }
}
