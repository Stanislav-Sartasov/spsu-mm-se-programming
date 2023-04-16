using System.Collections.Generic;
using System.Threading;
using ConsumerProducer;
using NUnit.Framework;

namespace ConsumerProducerTests
{
    public class ConsumerTests
    {
        [Test]
        public void Test()
        {
            var tasks = new List<Data<string>>();
            var locker = new TASLock();

            var prod = new Producer(locker, tasks, "produser 0");
            Thread.Sleep(100);
            prod.Join();

            var cons = new Consumer(locker, tasks, "consumer 0");
            Thread.Sleep(100);
            cons.Join();

            Assert.AreEqual(tasks.Count, 0);
        }
    }
}
