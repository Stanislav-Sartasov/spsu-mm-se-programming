public static class Program
{
    private static volatile int TaskCompleted = 1;

    public static void Main(string[] args)
    {
        Console.WriteLine("The program demonstrates how thread pool works!\n" +
                          "All threads have to count numbers from 1 to 100000");
        Console.Write("Desired number of threads: ");
        var threadNumber = Console.ReadLine();
        Console.Write("Desired number of tasks to schedule: ");
        var taskNumber = Console.ReadLine();

        if (!int.TryParse(threadNumber, out var capacity) || !int.TryParse(taskNumber, out var tasks))
        {
            Console.WriteLine("You are supposed to write numbers. Try again, please!");
            return;
        }

        var pool = new ThreadPool.ThreadPool(capacity);
        for (var i = 0; i < tasks; i++)
            pool.Enqueue(Count);
        pool.Dispose();
        Console.WriteLine("Thank you for testing the library!");
    }

    private static void Count()
    {
        var sum = 0;
        for (int i = 1; i < 100000; i++)
            sum += i;
        var newVal = Interlocked.Increment(ref TaskCompleted);
        Console.WriteLine($"({newVal - 1}) count result is {sum}");
    }
}