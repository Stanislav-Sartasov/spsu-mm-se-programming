using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using Task5.ExamSystem;

namespace Task5_Test
{
    public class ExamSystemsTests
    {
        IExamSystem[] systems = { new ExamHashTable(3), new ExamSet() };

        [SetUp]
        public void Setup()
        {
            systems[0] = new ExamHashTable(3);
            systems[1] = new ExamSet();
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void TestAddOneThread(int systemId)
        {
            IExamSystem sys = systems[systemId];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 10; j < 15; j++)
                {
                    sys.Add(i, j);

                    Assert.IsTrue(sys.Contains(i, j));

                    Assert.IsFalse(sys.Contains(j, i));
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 10; j < 15; j++)
                {
                    Assert.IsTrue(sys.Contains(i, j));

                    Assert.IsFalse(sys.Contains(j, i));
                }
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void TestRemoveOneThread(int systemId)
        {
            IExamSystem sys = systems[systemId];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 10; j < 15; j++)
                {
                    sys.Add(i, j);
                    sys.Add(i, j);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 10; j < 15; j++)
                {
                    sys.Remove(i, j);

                    Assert.IsFalse(sys.Contains(i, j));
                }

                for (int i1 = i + 1; i1 < 5; i1++)
                {
                    for (int j = 10; j < 15; j++)
                    {
                        Assert.IsTrue(sys.Contains(i1, j));
                    }
                }
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void TestCountOneThread(int systemId)
        {
            IExamSystem sys = systems[systemId];
            int cnt = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 10; j < 15; j++)
                {
                    sys.Add(i, j);
                    sys.Add(i, j);
                    cnt++;

                    Assert.That(cnt == sys.Count);
                }
            }

            sys.Remove(100, 100);
            Assert.That(cnt == sys.Count);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 10; j < 15; j++)
                {
                    sys.Remove(i, j);
                    sys.Remove(i, j);
                    cnt--;

                    Assert.That(cnt == sys.Count);
                }
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void TestAddMultyThreads(int systemId)
        {
            IExamSystem sys = systems[systemId];

            for (int i = 0; i < 5; i++)
            {
                Thread[] threads = new Thread[5];
                threads[0] = new Thread(new ThreadStart(() =>
                {
                    sys.Add(i, 10);
                    Assert.IsTrue(sys.Contains(i, 10));
                    Assert.IsFalse(sys.Contains(10, i));
                }));
                threads[1] = new Thread(new ThreadStart(() =>
                {
                    sys.Add(i, 11);
                    Assert.IsTrue(sys.Contains(i, 11));
                    Assert.IsFalse(sys.Contains(11, i));
                }));
                threads[2] = new Thread(new ThreadStart(() =>
                {
                    sys.Add(i, 12);
                    Assert.IsTrue(sys.Contains(i, 12));
                    Assert.IsFalse(sys.Contains(12, i));
                }));
                threads[3] = new Thread(new ThreadStart(() =>
                {
                    sys.Add(i, 13);
                    Assert.IsTrue(sys.Contains(i, 13));
                    Assert.IsFalse(sys.Contains(13, i));
                }));
                threads[4] = new Thread(new ThreadStart(() =>
                {
                    sys.Add(i, 14);
                    Assert.IsTrue(sys.Contains(i, 14));
                    Assert.IsFalse(sys.Contains(14, i));
                }));

                for (int j = 0; j < 5; j++)
                {
                    threads[j].Start();
                }
                for (int j = 0; j < 5; j++)
                {
                    threads[j].Join();
                }

                for (int j = 10; j < 15; j++)
                {
                    Assert.IsTrue(sys.Contains(i, j));
                    Assert.IsFalse(sys.Contains(j, i));
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 10; j < 15; j++)
                {
                    Assert.IsTrue(sys.Contains(i, j));
                    Assert.IsFalse(sys.Contains(j, i));
                }
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void TestRemoveMultyThreads(int systemId)
        {
            IExamSystem sys = systems[systemId];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 10; j < 15; j++)
                {
                    sys.Add(i, j);
                    sys.Add(i, j);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                Thread[] threads = new Thread[5];
                threads[0] = new Thread(new ThreadStart(() =>
                {
                    sys.Remove(i, 10);
                    Assert.IsFalse(sys.Contains(i, 10));
                }));
                threads[1] = new Thread(new ThreadStart(() =>
                {
                    sys.Remove(i, 11);
                    Assert.IsFalse(sys.Contains(i, 11));
                }));
                threads[2] = new Thread(new ThreadStart(() =>
                {
                    sys.Remove(i, 12);
                    Assert.IsFalse(sys.Contains(i, 12));
                }));
                threads[3] = new Thread(new ThreadStart(() =>
                {
                    sys.Remove(i, 13);
                    Assert.IsFalse(sys.Contains(i, 13));
                }));
                threads[4] = new Thread(new ThreadStart(() =>
                {
                    sys.Remove(i, 14);
                    Assert.IsFalse(sys.Contains(i, 14));
                }));

                for (int j = 0; j < 5; j++)
                {
                    threads[j].Start();
                }
                for (int j = 0; j < 5; j++)
                {
                    threads[j].Join();
                }

                for (int i1 = i + 1; i1 < 5; i1++)
                {
                    for (int j = 10; j < 15; j++)
                    {
                        Assert.IsTrue(sys.Contains(i1, j));
                    }
                }
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void TestCountMultyThreads(int systemId)
        {
            IExamSystem sys = systems[systemId];
            int cnt = 0;
            for (int i = 0; i < 5; i++)
            {
                Thread[] threads = new Thread[5];
                threads[0] = new Thread(new ThreadStart(() => sys.Add(i, 10)));
                threads[1] = new Thread(new ThreadStart(() => sys.Add(i, 11)));
                threads[2] = new Thread(new ThreadStart(() => sys.Add(i, 12)));
                threads[3] = new Thread(new ThreadStart(() => sys.Add(i, 13)));
                threads[4] = new Thread(new ThreadStart(() => sys.Add(i, 14)));

                for (int j = 0; j < 5; j++)
                {
                    threads[j].Start();
                }
                for (int j = 0; j < 5; j++)
                {
                    threads[j].Join();
                }

                cnt += 5;
                Assert.That(cnt == sys.Count);
            }

            sys.Remove(100, 100);
            Assert.That(cnt == sys.Count);
            for (int i = 0; i < 5; i++)
            {

                Thread[] threads = new Thread[5];
                threads[0] = new Thread(new ThreadStart(() => sys.Remove(i, 10)));
                threads[1] = new Thread(new ThreadStart(() => sys.Remove(i, 11)));
                threads[2] = new Thread(new ThreadStart(() => sys.Remove(i, 12)));
                threads[3] = new Thread(new ThreadStart(() => sys.Remove(i, 13)));
                threads[4] = new Thread(new ThreadStart(() => sys.Remove(i, 14)));

                for (int j = 0; j < 5; j++)
                {
                    threads[j].Start();
                }
                for (int j = 0; j < 5; j++)
                {
                    threads[j].Join();
                }

                cnt -= 5;
                Assert.That(cnt == sys.Count);
            }
        }
    }
}