using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FuturesAsync
{
    class Program
    {
        private static long GetSomeTriangleNumber()
        {
            Console.WriteLine($"Task thread is {Thread.CurrentThread.ManagedThreadId}");
            long result = 0;
            for (long i = 1; i <= 30000000; i++)
            {
                result += i;
            }
            return result;
        }

        static async void DoSomething()
        {
            Task<long> firstTask = new Task<long>(GetSomeTriangleNumber);
            firstTask.Start();
            Console.WriteLine($"Beginning of DoSomething thread is {Thread.CurrentThread.ManagedThreadId}");
            long result = await firstTask;

            Task<long> secondTask = new Task<long>(GetSomeTriangleNumber);
            secondTask.Start();
            Console.WriteLine($"Middle of DoSomething thread is {Thread.CurrentThread.ManagedThreadId}");
            result = await secondTask;
            Console.WriteLine($"End of DoSomething thread is {Thread.CurrentThread.ManagedThreadId}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"Beginning of Main thread is {Thread.CurrentThread.ManagedThreadId}");
            DoSomething();
            Console.WriteLine($"End of Main thread is {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadKey();
        }

        //static void Main(string[] args)
        //{
        //    Task<long> task = new Task<long>(() =>
        //    {
        //        Console.WriteLine($"Task thread is {Thread.CurrentThread.ManagedThreadId}");
        //        return GetSomeTriangleNumber();
        //    });

        //    task.Start();
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();

        //    var outright = task.IsCompleted ? "" : "not ";
        //    Console.WriteLine($"The task is {outright} finished outright");
        //    Console.WriteLine($"Main thread is {Thread.CurrentThread.ManagedThreadId}");

        //    //task.Wait();

        //    Console.WriteLine(task.Result);
        //    sw.Stop();
        //    Console.WriteLine(sw.ElapsedMilliseconds);
        //}
    }
}
