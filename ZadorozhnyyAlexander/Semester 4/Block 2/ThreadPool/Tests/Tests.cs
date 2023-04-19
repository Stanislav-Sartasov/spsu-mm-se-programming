using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;

namespace Tests
{
    public class Tests
    {
        static Random random = new Random();

        public static Action TestAction = () =>
        {
            var task = random.Next(100);
            Console.WriteLine($"--> Start do task {task}...");
            Thread.Sleep(random.Next(10));
            Console.WriteLine($"--> Task {task} was finished.");
        };

        [Test]
        public void ZeroThreadPoolTest()
        {
            try
            {
                using (var threadPool = new ThreadPool.ThreadPool(0))
                {
                    for (int i = 0; i < 10; i++)
                        threadPool.EnqueueTask(TestAction);
                }
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.Pass();
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void MultiThreadPoolTest()
        {
            
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                using (var threadPool = new ThreadPool.ThreadPool(10))
                {
                    for (int i = 0; i < 1000; i++)
                        threadPool.EnqueueTask(TestAction);
                }
                stopwatch.Stop();
                Assert.Less(stopwatch.ElapsedMilliseconds, 100);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void DisposeThreadPoolTest()
        {
            try
            {
                var threadPool = new ThreadPool.ThreadPool(10);

                for (int i = 0; i < 1000; i++)
                    threadPool.EnqueueTask(TestAction);

                threadPool.Dispose();
                threadPool.EnqueueTask(TestAction);
                Assert.Fail();
            }
            catch (InvalidOperationException e)
            {
                Assert.Pass();
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}