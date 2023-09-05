using System;

namespace Task_1
{
    public class Program
    {
        public static Random Random = new Random();

        public static Func<int> ProducerFunc = () =>
        {
            int randInt = Random.Next(10000);
            Console.WriteLine("value {0} was generated", randInt);
            return randInt;
        };

        static void Main(string[] args)
        {
            ThreadsManager<int> newManager = new ThreadsManager<int>(ProducerFunc);
            newManager.Start();
            Console.ReadKey();
            newManager.Stop();
        }
    }
}