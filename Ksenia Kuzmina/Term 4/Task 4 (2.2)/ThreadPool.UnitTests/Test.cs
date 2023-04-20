using NUnit.Framework;
using System.Threading;
using System;

namespace ProducerConsumer.UnitTests
{
    public class Tests
    {
        [Test]
        public void ThreadPoolTest()
        {
            var counter = 0;

            void Increment(ref int counter)
            {
                counter++;
            }

            using ThreadPool.ThreadPool pool = new ThreadPool.ThreadPool();

            for (var i = 0; i < 5; i++)
                pool.Enqueue(() => Increment(ref counter));

            Thread.Sleep(1000);

            Assert.AreEqual(counter, 5);
        }
    }
}

