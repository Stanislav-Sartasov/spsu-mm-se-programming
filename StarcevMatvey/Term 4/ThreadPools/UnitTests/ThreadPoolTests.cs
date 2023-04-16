using System;
using System.Threading;
using NUnit.Framework;
using ThreadPool;

namespace ThreadPooltests
{
    public class ThreadPoolTests
    {
        private static int i = 0;

        [Test]
        public void Test()
        {
            Action act = () => i++;

            var n = 4;
            var pool = new ThreadPool.ThreadPool(n);

            for(var i = 0; i < 10; i++) pool.Enqueue(act);
            Thread.Sleep(3000);

            pool.Dispose();

            Assert.AreEqual(i, 10);
        }

        [Test]
        public void StopTest()
        {
            Action act = () => i++;

            var n = 1;
            var pool = new ThreadPool.ThreadPool(n);

            pool.Dispose();

            Assert.Catch(() => pool.Enqueue(act));
        }
    }
}