namespace Tests
{
    public class ThreadPoolTests
    {
        [Test]
        public void EnqueueAfterDisposeTest()
        {
            var tp = new ThreadPool.ThreadPool();

            for (int i = 0; i < 10; i++)
            {
                tp.Enqueue(() => { Thread.Sleep(50); });
            }

            tp.Dispose();

            Assert.Catch<InvalidOperationException>(() => tp.Enqueue(() => {}));
        }

        [Test]
        public void DoubleDisposeTest()
        {
            var tp = new ThreadPool.ThreadPool();

            for (int i = 0; i < 10; i++)
            {
                tp.Enqueue(() => { Thread.Sleep(50); });
            }

            tp.Dispose();
            tp.Dispose();

            Assert.Pass();
        }

        [Test]
        public void NullActionsTest()
        {
            var tp = new ThreadPool.ThreadPool();

            for (int i = 0; i < 10; i++)
            {
                tp.Enqueue(null);
            }

            tp.Dispose();

            Assert.Pass();
        }


        private volatile int x = 0;

        [Test]
        public void ActionsCountLtThreadsCountTest()
        {
            var tp = new ThreadPool.ThreadPool();

            tp.Enqueue(() => Interlocked.Increment(ref x));
            tp.Enqueue(() => Interlocked.Decrement(ref x));

            tp.Dispose();

            Assert.AreEqual(0, x);
        }

        private volatile int sum = 0;

        [Test]
        public void ActionsCountGtThreadsCountTest()
        {
            var trueSum = Enumerable.Range(1, 100).Select(i => i * i).Sum();

            using (var tp = new ThreadPool.ThreadPool())
            {
                for (int i = 1; i < 101; i++)
                {
                    var value = i;
                    tp.Enqueue(() =>
                    {
                        Thread.Sleep(value);
                        Interlocked.Add(ref sum, value * value);
                    });
                }
            }

            Assert.AreEqual(trueSum, sum);
        }
    }
}