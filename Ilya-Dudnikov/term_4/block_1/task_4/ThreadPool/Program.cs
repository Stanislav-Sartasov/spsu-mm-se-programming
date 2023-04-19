namespace ThreadPool;

public class Program
{
    public static void Main(string[] args)
    {
        var threadCount = 5;
        var taskCount = 100;
        var random = new Random();

        ThreadPool threadPool = new(threadCount);
        for (var i = 0; i < taskCount; i++)
            threadPool.Enqueue(() =>
            {
                Thread.Sleep(random.Next(200)); // work emulation
            });

        Console.WriteLine("Running a thread pool...");

        threadPool.Start();
        Thread.Sleep(100);
        threadPool.Dispose();

        Console.WriteLine("Thread pool finished successfully!");
    }
}