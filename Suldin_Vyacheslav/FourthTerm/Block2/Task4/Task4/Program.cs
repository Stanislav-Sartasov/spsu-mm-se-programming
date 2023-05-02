
namespace Task4
{

    public class Program
    {
        public static int Main(string[] args)
        {
            ThreadPool pool = new ThreadPool();
            for (int i = 0; i < 20; i++)
            {
                pool.Enqueue(() => Console.WriteLine($"[{Environment.CurrentManagedThreadId}] completed task"));
            }
            pool.Dispose();
            return 0;
        }

    }
}