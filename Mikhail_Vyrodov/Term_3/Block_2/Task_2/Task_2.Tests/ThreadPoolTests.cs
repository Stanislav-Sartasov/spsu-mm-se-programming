using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Task_2.Tests
{
    public class ThreadPoolTests
    {
        private static Random random = new Random();

        private static int completedTaskCounter = 0;

        private static void Task()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started doing some task.");
            Thread.Sleep(random.Next(200));
            completedTaskCounter += 1;
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished its task.");
        }

        [Test]
        public void ExceptionAfterDisposeTest()
        {
            try
            {
                completedTaskCounter = 0;
                ThreadPool threadPool = new ThreadPool(5);
                for (int i = 0; i < 30; i++)
                {
                    threadPool.Enqueue(Task);
                }

                Thread.Sleep((30 * 200) / 5);

                threadPool.Dispose();
                threadPool.Enqueue(Task);
                Assert.Fail();
            }
            catch (Exception e)
            {
                string message = "Trying to call enqueue method when pool was already disposed.";
                Assert.AreEqual(e.Message, message);
            }
        }

        [Test]
        public void PoolTest()
        {
            try
            {
                completedTaskCounter = 0;
                ThreadPool threadPool = new ThreadPool(5);
                for (int i = 0; i < 30; i++)
                {
                    threadPool.Enqueue(Task);
                }

                Thread.Sleep((30 * 200) / 5);

                threadPool.Dispose();
                Assert.AreEqual(completedTaskCounter, 30);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }
    }
}