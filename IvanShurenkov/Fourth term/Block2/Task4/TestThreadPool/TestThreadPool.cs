using NUnit.Framework;
using Task4;

namespace TestThreadPool
{
    public class Tests
    {
        private Mutex mutex;
        private int cnt;

        private void SomeAction()
        {
            mutex.WaitOne();
            cnt++;
            mutex.ReleaseMutex();
        }

        [SetUp]
        public void Setup()
        {
            mutex = new Mutex();
            cnt = 0;
        }

        [Test]
        public void TestOneThread()
        {
            int cntThreads = 1;
            int cntActions = 10;

            Task4.ThreadPool pool = new Task4.ThreadPool(cntThreads);
            for (int i = 0; i < cntActions; i++)
            {
                pool.Enqueue(SomeAction);
            }
            Thread.Sleep(50);
            pool.Dispose();
            
            Thread.Sleep(50);
            mutex.WaitOne();
            Assert.That(cnt, Is.EqualTo(cntActions));
            mutex.ReleaseMutex();
        }

        [Test]
        public void TestOneThreadWithMutex()
        {
            mutex.WaitOne();
            int cntThreads = 1;
            int cntActions = 10;

            Task4.ThreadPool pool = new Task4.ThreadPool(cntThreads);
            for(int i = 0; i < cntActions; i++)
            {
                pool.Enqueue(SomeAction);
            }
            Thread disposePool = new Thread(pool.Dispose);
            disposePool.Start();

            Thread.Sleep(50);
            mutex.ReleaseMutex();

            disposePool.Join();

            mutex.WaitOne();
            Assert.That(cnt, Is.EqualTo(cntThreads));
            mutex.ReleaseMutex();
        }

        [Test]
        public void TestFourThread()
        {
            int cntThreads = 4;
            int cntActions = 10;

            Task4.ThreadPool pool = new Task4.ThreadPool(cntThreads);
            for (int i = 0; i < cntActions; i++)
            {
                pool.Enqueue(SomeAction);
            }
            Thread.Sleep(50);
            pool.Dispose();
            
            Thread.Sleep(50);
            mutex.WaitOne();
            Assert.That(cnt, Is.EqualTo(cntActions));
            mutex.ReleaseMutex();
        }

        [Test]
        public void TestFourThreadWithMutex()
        {
            mutex.WaitOne();
            int cntThreads = 4;
            int cntActions = 10;

            Task4.ThreadPool pool = new Task4.ThreadPool(cntThreads);
            for (int i = 0; i < cntActions; i++)
            {
                pool.Enqueue(SomeAction);
            }
            Thread disposePool = new Thread(pool.Dispose);
            disposePool.Start();

            Thread.Sleep(50);
            mutex.ReleaseMutex();

            disposePool.Join();

            mutex.WaitOne();
            Assert.That(cnt, Is.EqualTo(cntThreads));
            mutex.ReleaseMutex();
        }
    }
}