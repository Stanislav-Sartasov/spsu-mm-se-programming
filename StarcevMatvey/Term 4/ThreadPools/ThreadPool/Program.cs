using System;

namespace ThreadPool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1) throw new Exception("Not enough arguments");

            var count = Utils.GetPositiveInt(args[0]);

            if (count <= 0) throw new Exception("First argument must be positive integer");

            var act = HelloThere;

            var pool = new ThreadPool(count);
            for (var i = 0; i < 10; i++) pool.Enqueue(act);
            Thread.Sleep(1000);

            pool.Dispose();
            Console.ReadKey();
        }

        private static void HelloThere()
        {
            Console.WriteLine("Hello there");
        }
    }
}