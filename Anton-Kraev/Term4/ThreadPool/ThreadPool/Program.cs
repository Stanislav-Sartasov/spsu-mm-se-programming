namespace ThreadPool;

internal class Program
{
    public static void Main()
    {
        var x1 = DateTime.Now;
        using (var tp = new ThreadPool())
        {
            tp.Enqueue(() => { Console.WriteLine($"das1 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
            tp.Enqueue(() => { Console.WriteLine($"das2 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
            tp.Enqueue(() => { Console.WriteLine($"das3 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
            tp.Enqueue(() => { Console.WriteLine($"das4 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
            tp.Enqueue(() => { Console.WriteLine($"das5 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
            tp.Enqueue(() => { Console.WriteLine($"das6 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
            tp.Enqueue(() => { Console.WriteLine($"das7 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
            tp.Enqueue(() => { Console.WriteLine($"das8 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
            tp.Enqueue(() => { Console.WriteLine($"das9 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
            tp.Enqueue(() => { Console.WriteLine($"das10 on {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(500); });
        }
        var x2 = DateTime.Now;

        Console.WriteLine(x2 - x1);
    }
}