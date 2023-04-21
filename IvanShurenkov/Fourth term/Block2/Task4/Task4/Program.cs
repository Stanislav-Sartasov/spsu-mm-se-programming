namespace Task4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const int cntThreads = 2;
            const int cntActions = 20;

            ThreadPool threadPool = new ThreadPool(cntThreads);

            for (int i = 0; i < cntActions; i++)
            {
                threadPool.Enqueue(() => { Thread.Sleep(100); Console.WriteLine("Action completed by some thread"); });
            }

            Console.ReadKey(true);
            threadPool.Dispose();
        }
    }
}