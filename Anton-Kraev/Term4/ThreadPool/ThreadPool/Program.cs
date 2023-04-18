using System.Diagnostics;

namespace ThreadPool;

internal class Program
{
    public static void Task(int i)
    {
        Thread.Sleep(500);
        Console.WriteLine($"Task {i} completed on thread {Thread.CurrentThread.ManagedThreadId}");
    }

    public static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();

        using (var tp = new ThreadPool())
        {
            for (var i = 0; i < 10; i++)
            {
                var taskIndex = i;
                tp.Enqueue(() => Task(taskIndex));
            }
        }

        stopwatch.Stop();

        Console.WriteLine($"\nAll tasks completed in {stopwatch.ElapsedMilliseconds}ms");
    }
}