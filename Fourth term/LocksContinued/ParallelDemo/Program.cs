using System;
using System.Diagnostics;
using System.Threading;

namespace ParallelDemo
{
    public interface ILock
    {
        void Lock();

        void Unlock();
    }

    public class SimpleLock : ILock
    {
        private object obj = new object();
        public void Lock()
        {
            Monitor.Enter(obj);
        }

        public void Unlock()
        {
            Monitor.Exit(obj);
        }
    }
    class Program
    {
        static long counter = 0;

        static ILock _lock = new SimpleLock();

        static void ThreadFuncAtomic()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Interlocked.Add(ref counter, i);
            }
        }

        static void DemoMethodAtomic()
        {
            counter = 0;
            var threads = new[]
            {
                new Thread(ThreadFuncAtomic),
                new Thread(ThreadFuncAtomic),
                new Thread(ThreadFuncAtomic),
            };

            var sw = new Stopwatch();
            sw.Start();

            foreach (var t in threads)
            {
                t.Start();
            }

            foreach (var t in threads)
            {
                t.Join();
            }
            sw.Stop();

            Console.WriteLine($"Calculated {counter} in {sw.ElapsedMilliseconds} ms");
        }

        static void ThreadFuncNoSync()
        {
            for (int i = 0; i < 1000000; i++)
            {
                //_lock.Lock();
                counter += i;
                //_lock.Unlock();
            }
        }

        static void DemoMethodNoSync()
        {
            counter = 0;
            var threads = new[]
            {
                new Thread(ThreadFuncNoSync),
                new Thread(ThreadFuncNoSync),
                new Thread(ThreadFuncNoSync),
            };

            var sw = new Stopwatch();
            sw.Start();

            foreach (var t in threads)
            {
                t.Start();
            }

            foreach (var t in threads)
            {
                t.Join();
            }
            sw.Stop();

            Console.WriteLine($"Calculated {counter} in {sw.ElapsedMilliseconds} ms");
        }

        static void ThreadFunc()
        {
            for (int i = 0; i < 1000000; i++)
            {
                _lock.Lock();
                counter += i;
                _lock.Unlock();
            }
        }

        static void DemoMethod()
        {
            counter = 0;
            var threads = new[]
            {
                new Thread(ThreadFunc),
                new Thread(ThreadFunc),
                new Thread(ThreadFunc),
            };

            var sw = new Stopwatch();
            sw.Start();

            foreach (var t in threads)
            {
                t.Start();
            }

            foreach (var t in threads)
            {
                t.Join();
            }
            sw.Stop();

            Console.WriteLine($"Calculated {counter} in {sw.ElapsedMilliseconds} ms");
        }

        static void ThreadFuncLocalCounter()
        {
            long localCounter = 0;
            for (int i = 0; i < 1000000; i++)
            {
                localCounter += i;
            }
            _lock.Lock();
            counter += localCounter;
            _lock.Unlock();
        }

        static void DemoMethodLocalCounter()
        {
            counter = 0;
            var threads = new[]
            {
                new Thread(ThreadFuncLocalCounter),
                new Thread(ThreadFuncLocalCounter),
                new Thread(ThreadFuncLocalCounter),
            };

            var sw = new Stopwatch();
            sw.Start();

            foreach (var t in threads)
            {
                t.Start();
            }

            foreach (var t in threads)
            {
                t.Join();
            }
            sw.Stop();

            Console.WriteLine($"Calculated {counter} in {sw.ElapsedMilliseconds} ms");
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                DemoMethodNoSync();
            }

            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                DemoMethod();
            }

            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                DemoMethodLocalCounter();
            }

            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                DemoMethodAtomic();
            }
        }
    }
}
