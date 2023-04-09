namespace ThreadPool;

public class Program
{
    static Random random = new Random();

    private static Func<string> GenarateTask = () =>
    {
        var length = random.Next(100);
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    };

    private static Action GenerateTask = () =>
    {
        var task = GenarateTask();
        Console.WriteLine($"--> Start do task {task}...");
        Thread.Sleep(random.Next(1000));
        Console.WriteLine($"--> Task {task} was finished.");
    };

    public static int Main(string[] args)
    {
        try
        {
            using (var threadPool = new ThreadPool(10))
            {
                for (int i = 0; i < 20; i++)
                    threadPool.EnqueueTask(GenerateTask);

                Console.ReadKey();
            }

            Console.ReadKey();
            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occured: {e}");
            return -1;
        }
    }
}