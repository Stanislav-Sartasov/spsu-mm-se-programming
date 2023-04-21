using System.Threading;
using Task3;

namespace Task3.UnitTests
{
    public class UnitTest
    {

        [Test]
        public void ConsumerTest()
        {
            var numbers = new List<int>() { 1, 2, 3 };
            var consumer = new Consumer<int>(numbers);
            consumer.Start();
            Thread.Sleep(600);
            consumer.Stop();
            Assert.AreEqual(2, numbers.Count());
        }

        [Test]
        public void ProducerTest()
        {
            var numbers = new List<int>() { 1, 2, 3 };
            var producer = new Producer<int>(numbers);
            producer.Start();
            Thread.Sleep(600);
            producer.Stop();
            Assert.AreEqual(4, numbers.Count());
        }

        [Test]
        public void Combined()
        {
            List<int> buffer = new List<int>();

            var manager = new Manager<int>(3, 3, buffer);

            manager.Manage();
            Thread.Sleep(10000);
            manager.ShoutDown();
        }
    }
}