using ProducerConsumer;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System;

namespace Tests
{
    public class Tests
    {
        Random random = new Random();

        [Test]
        public void ManagerTest()
        {
            var manager = new Manager<List<int>>(4, 5, 10, () => new List<int> { random.Next(100), random.Next(1000) });

            manager.StartWork();
            Thread.Sleep(3000);
            manager.StopWork();

            Assert.Pass();
        }

        [Test]
        public void ConsumerTest()
        {
            var semaphore = new Semaphore(1, 1);
            var applications = Enumerable.Range(0, 10).ToList();

            var consumer = new Consumer<int>(semaphore, 100, applications);
            consumer.Start();
            Thread.Sleep(1);
            consumer.Stop();

            Assert.AreEqual(Enumerable.Range(1, 9).ToList(), applications);
        }

        [Test]
        public void ProducerTest()
        {
            var semaphore = new Semaphore(1, 1);
            var applications = new List<int>();

            var producer = new Producer<int>(semaphore, 100, applications, () => random.Next(10));
            producer.Start();
            Thread.Sleep(1);
            producer.Stop();

            Assert.AreEqual(1, applications.Count);
        }
    }
}