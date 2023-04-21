using System;

namespace Task_2
{
    internal class Program
    {
        private static Random random = new Random();

        private static void Task()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started doing some task.");
            Thread.Sleep(random.Next(500));
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished its task.");
        }

        static void Main(string[] args)
        {
            const int threadsCount = 5;

            using (ThreadPool threadPool = new ThreadPool(threadsCount))
            {
                for (int i = 0; i < 30; i++)
                {
                    threadPool.Enqueue(Task);
                }

                Thread.Sleep(15000);
            }
        }
    }
}