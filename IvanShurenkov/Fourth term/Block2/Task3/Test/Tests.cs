using NUnit.Framework;
using Task3.Locks;
using Task3.ProducerConsumer;
using Task3;

namespace Test
{
    public class Tests
    {
        private volatile List<int> buffer;
        private volatile List<int> items;
        private static Random Rnd;
        private ILock localLocker;

        [SetUp]
        public void Setup()
        {
            buffer = new List<int>();
            items = new List<int>();
            Rnd = new Random();
            localLocker = new TTASLock();
        }

        [Test]
        public void TestProducer()
        {
            Producer<int> producer = new Producer<int>(buffer, localLocker, () => { int item = Rnd.Next(); items.Add(item); return item; });
            producer.Start();
            Thread.Sleep(5);
            producer.Stop();

            Assert.That(buffer, Is.EqualTo(items));
        }

        [Test]
        public void TestTwoProducers()
        {
            List<Producer<int>> producers = new List<Producer<int>>();
            for (int i = 0; i < 2; i++)
            {
                producers.Add(new Producer<int>(buffer, localLocker, () => { int item = Rnd.Next(); Thread.Sleep(100); return item; }));
            }
            for (int i = 0; i < producers.Count; i++)
            {
                producers[i].Start();
            }
            Thread.Sleep(10);
            for (int i = 0; i < producers.Count; i++)
            {
                producers[i].Stop();
            }

            Thread.Sleep(150);

            Assert.That(buffer.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestConsumer()
        {
            Consumer<int> consumer = new Consumer<int>(buffer, localLocker, items.Add);
            List<int> expected = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                expected.Add(i);
                buffer.Add(i);
            }
            consumer.Start();
            while (buffer.Count > 0)
            { Thread.Sleep(0); }
            consumer.Stop();

            Assert.That(items, Is.EqualTo(expected));
        }

        [Test]
        public void TestTwoConsumers()
        {
            List<Consumer<int>> consumers = new List<Consumer<int>>();
            List<int>[] consumerItems = new List<int>[2];
            for (int i = 0; i < consumerItems.Length; i++)
            {
                consumerItems[i] = new List<int>();
            }
            consumers.Add(new Consumer<int>(buffer, localLocker, item => { consumerItems[0].Add(item); }));
            consumers.Add(new Consumer<int>(buffer, localLocker, item => { consumerItems[1].Add(item); }));

            for (int i = 0; i < 10; i++)
            {
                buffer.Add(i);
            }
            for (int i = 0; i < consumers.Count; i++)
            {
                consumers[i].Start();
            }
            Thread.Sleep(50);

            for (int i = 0; i < consumers.Count; i++)
            {
                consumers[i].Stop();
            }

            Thread.Sleep(20);

            int cnt = 0;
            for (int i = 0; i < consumerItems.Length; i++)
            {
                cnt += consumerItems[i].Count;
                for (int j = 0; j < consumerItems[i].Count; j++)
                {
                    for (int k = 0; k < consumerItems.Length; k++)
                    {
                        if (k == i)
                            continue;
                        Assert.That(consumerItems[k].FindIndex(0, item => { return item == consumerItems[i][j]; }), Is.EqualTo(-1));
                    }
                }
            }
            Assert.That(cnt, Is.EqualTo(10));
        }

        [Test]
        public void TestFourConsumers()
        {
            List<Consumer<int>> consumers = new List<Consumer<int>>();
            List<int>[] consumerItems = new List<int>[4];
            for (int i = 0; i < consumerItems.Length; i++)
            {
                consumerItems[i] = new List<int>();
            }
            consumers.Add(new Consumer<int>(buffer, localLocker, item => { consumerItems[0].Add(item); }));
            consumers.Add(new Consumer<int>(buffer, localLocker, item => { consumerItems[1].Add(item); }));
            consumers.Add(new Consumer<int>(buffer, localLocker, item => { consumerItems[2].Add(item); }));
            consumers.Add(new Consumer<int>(buffer, localLocker, item => { consumerItems[3].Add(item); }));

            for (int i = 0; i < 10; i++)
            {
                buffer.Add(i);
            }
            for (int i = 0; i < consumers.Count; i++)
            {
                consumers[i].Start();
            }
            Thread.Sleep(50);

            for (int i = 0; i < consumers.Count; i++)
            {
                consumers[i].Stop();
            }

            Thread.Sleep(20);

            int cnt = 0;
            for (int i = 0; i < consumerItems.Length; i++)
            {
                cnt += consumerItems[i].Count;
                for (int j = 0; j < consumerItems[i].Count; j++)
                {
                    for (int k = 0; k < consumerItems.Length; k++)
                    {
                        if (k == i)
                            continue;
                        Assert.That(consumerItems[k].FindIndex(0, item => { return item == consumerItems[i][j]; }), Is.EqualTo(-1));
                    }
                }
            }
            Assert.That(cnt, Is.EqualTo(10));
        }

        [Test]
        public void TestManager()
        {
            ProducerConsumerManager manager = new ProducerConsumerManager(3, 5);
            
            Assert.That(manager.Consumers.Count, Is.EqualTo(5));
            Assert.That(manager.Producers.Count, Is.EqualTo(3));

            manager.Start();

            for (int i = 0; i < manager.Consumers.Count; i++)
            {
                Assert.IsFalse(manager.Consumers[i].GetState());
            }

            for (int i = 0; i < manager.Producers.Count; i++)
            {
                Assert.IsFalse(manager.Producers[i].GetState());
            }

            manager.Stop();

            for (int i = 0; i < manager.Consumers.Count; i++)
            {
                Assert.IsTrue(manager.Consumers[i].GetState());
            }

            for (int i = 0; i < manager.Producers.Count; i++)
            {
                Assert.IsTrue(manager.Producers[i].GetState());
            }
        }
    }
}