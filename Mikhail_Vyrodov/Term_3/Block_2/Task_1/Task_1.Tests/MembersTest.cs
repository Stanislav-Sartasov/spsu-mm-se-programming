using System;

namespace Task_1.Tests
{
    public class Tests
    {
        public static int Counter = 0;

        public static Func<int> ProducerFunc = () =>
        {
            return Counter++;
        };

        [Test]
        public void ProducerTest()
        {
            Mutex mut = new Mutex();
            List<int> values = new List<int>();
            int pauseInterval = 100;
            Producer<int> newProducer = new Producer<int>(pauseInterval, values, ProducerFunc, mut);
            newProducer.Run();
            Thread.Sleep(pauseInterval * 5);
            newProducer.Stop();
            string expectedStr = "0 1 2 3 4";
            Assert.AreEqual(expectedStr, newProducer.ValuesToString());
        }

        [Test]
        public void ConsumerTest()
        {
            Mutex mut = new Mutex();
            List<int> values = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                values.Add(i);
            }
            int pauseInterval = 100;
            Consumer<int> newConsumer = new Consumer<int>(100, values, mut);
            newConsumer.Run();
            Thread.Sleep(500);
            Assert.AreEqual(1, newConsumer.GetValuesLen());
            Thread.Sleep(100);
            newConsumer.Stop();
            Assert.AreEqual(0, newConsumer.GetValuesLen());
            Assert.Pass();
        }
    }
}