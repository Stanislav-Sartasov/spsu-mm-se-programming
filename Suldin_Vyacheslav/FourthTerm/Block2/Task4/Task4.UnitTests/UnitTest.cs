using System.Text.RegularExpressions;
using System;
using System.Threading;
using Task4;

namespace Task4.UnitTests
{
    public class Task
    {
        public bool isDone = false;
    }

    public class UnitTest
    {
        [Test]
        public void TPCorrectnessTest()
        {
            int taskCount = 10;
            int sum = 0;
            var tp = new ThreadPool();
            for (int i = 0; i < taskCount; i++)
            {
                tp.Enqueue(() => sum++);
            }
            tp.Dispose();

            Assert.That(sum, Is.EqualTo(taskCount));
        }

        [Test]
        public void WithExeptionTask()
        {
            int taskCount = 10;
            int sum = 0;
            var tp = new ThreadPool();
            for (int i = 0; i < taskCount; i++)
            {
                if (i == 0)
                {
                    tp.Enqueue(() => throw new Exception());
                }
                else tp.Enqueue(() => sum++);
            }
            tp.Dispose();

            Assert.That(sum, Is.EqualTo(taskCount - 1));
        }

        [Test]
        public void LessTasks()
        {
            int taskCount = 3;
            int sum = 0;
            var tp = new ThreadPool();
            for (int i = 0; i < taskCount; i++)
            {
                tp.Enqueue(() => sum++);
            }
            tp.Dispose();

            Assert.That(sum, Is.EqualTo(taskCount));
        }

        [Test]
        public void DisposeTest()
        {
            var tp = new ThreadPool();
            for (int i = 0; i < 5; i++)
            {
                tp.Enqueue(Task);
            }
            tp.Dispose();

            Assert.True(!tp.actions.Any());
            Assert.True(!tp.threads.Any());
        }

        [Test]
        public void DisposedEnqueueTest()
        {
            var tp = new ThreadPool();

            tp.Enqueue(Task);
            tp.Dispose();

            Assert.Throws<InvalidOperationException>(() => tp.Enqueue(Task));
        }

        private void Task()
        {
            Random rnd = new Random();
            Thread.Sleep(rnd.Next(2000));
        }
    }
}