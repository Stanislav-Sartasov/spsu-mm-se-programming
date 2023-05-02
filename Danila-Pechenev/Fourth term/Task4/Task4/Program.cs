namespace Task4;

class Program
{
    static int Main(string[] args)
    {
        int numberOfThreads = 8;
        int numberOfActions = 30;

        var pool = new ThreadPool(numberOfThreads);
        for (int i = 0; i < numberOfActions; i++)
        {
            pool.Enqueue(SomeAction);
        }

        Thread.Sleep(numberOfActions * 150);
        pool.Dispose();

        return 0;
    }

    private static void SomeAction()
    {
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} performs some action.");
        Thread.Sleep(new Random().Next(5, 300));
    }
}
