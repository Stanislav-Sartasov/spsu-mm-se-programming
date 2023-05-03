using NUnit.Framework;
using DeansOffice;
using System.Diagnostics;

namespace DeansOfficeTest
{
    public class Tests
    {
        [Test]
        public void AddLockFreeSetExamSystemTest()
        {
            IExamSystem examSystemS = new ExamSystemS();

            for (int i = 0; i < 10; i++)
                examSystemS.Add(i, i);

            Assert.AreEqual(10, examSystemS.Count);
        }

        [Test]
        public void RemoveLockFreeSetExamSystemTest()
        {
            IExamSystem examSystemS = new ExamSystemS();

            for (int i = 0; i < 10; i++)
                examSystemS.Add(i, i);

            for (int j = 0; j < 10; j++)
                examSystemS.Remove(j, j);

            Assert.AreEqual(0, examSystemS.Count);
        }

        [Test]
        public void ContainsLockFreeSetExamSystemTest()
        {
            IExamSystem examSystemS = new ExamSystemS();

            for (int i = 0; i < 10; i++)
                examSystemS.Add(i, i);

            for (int j = 0; j < 10; j++)
            {
                if (!examSystemS.Contains(j, j))
                    Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void CountLockFreeSetExamSystemTest()
        {
            IExamSystem examSystemS = new ExamSystemS();

            for (int i = 0; i < 10; i++)
                examSystemS.Add(i, i);

            Assert.AreEqual(10, examSystemS.Count);
        }

        [Test]
        public void EfficiencyLockFreeSetExamSystemTest()
        {
            IExamSystem examSystemS = new ExamSystemS();

            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < 1000; i++)
                examSystemS.Add(i, i);

            stopwatch.Stop();
            Assert.Less(stopwatch.ElapsedMilliseconds, 100);
        }

        [Test]
        public void AddLockFreeHashSetExamSystemTest()
        {
            IExamSystem examSystemH = new ExamSystemH();

            for (int i = 0; i < 10; i++)
                examSystemH.Add(i, i);

            Assert.AreEqual(10, examSystemH.Count);
        }

        [Test]
        public void RemoveLockFreeHashSetExamSystemTest()
        {
            IExamSystem examSystemH = new ExamSystemH();

            for (int i = 0; i < 10; i++)
                examSystemH.Add(i, i);

            for (int j = 0; j < 10; j++)
                examSystemH.Remove(j, j);

            Assert.AreEqual(0, examSystemH.Count);
        }

        [Test]
        public void ContainsLockFreeHashSetExamSystemTest()
        {
            IExamSystem examSystemH = new ExamSystemH();

            for (int i = 0; i < 10; i++)
                examSystemH.Add(i, i);

            for (int j = 0; j < 10; j++)
            {
                if (!examSystemH.Contains(j, j))
                    Assert.Fail();
            }
            Assert.Pass();
        }

        [Test]
        public void CountLockFreeHashSetExamSystemTest()
        {
            IExamSystem examSystemH = new ExamSystemH();

            for (int i = 0; i < 10; i++)
                examSystemH.Add(i, i);

            Assert.AreEqual(10, examSystemH.Count);
        }

        [Test]
        public void EfficiencyLockFreeHashSetExamSystemTest()
        {
            IExamSystem examSystemH = new ExamSystemH();

            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < 1000; i++)
                examSystemH.Add(i, i);

            stopwatch.Stop();
            Assert.Less(stopwatch.ElapsedMilliseconds, 100);
        }
    }
}