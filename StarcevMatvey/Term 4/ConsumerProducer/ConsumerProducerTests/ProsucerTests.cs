using System.Collections.Generic;
using System.Threading;
using ConsumerProducer;
using NUnit.Framework;

namespace ConsumerProducerTests
{
    public class ProsucerTests
    {
        [Test]
        public void Test()
        {
            var tasks = new List<Data<string>>();
            var prod = new Producer(tasks, "produser 0");
            Thread.Sleep(100);
            prod.Join();

            Assert.AreEqual(tasks.Count, 1);
        }
    }
}
