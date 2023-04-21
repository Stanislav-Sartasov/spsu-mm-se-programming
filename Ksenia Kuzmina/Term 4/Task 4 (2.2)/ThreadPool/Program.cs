namespace ThreadPool
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            void Output()
            {
                Console.WriteLine("Hello, " + Environment.CurrentManagedThreadId);
            }

            ThreadPool pool = new ThreadPool();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    pool.Enqueue(Output);
                }

                Thread.Sleep(1000);
            }

            pool.Dispose();
        }
    }
}