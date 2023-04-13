using Task3.Locks;
using Task3.ProducerConsumer;

namespace Task3
{
    public class ProducerConsumerManager
    {
        private volatile List<int> buffer;
        private ILock commonLocker;
        public List<Producer<int>> Producers;
        public List<Consumer<int>> Consumers;

        public ProducerConsumerManager(int cntProducer, int cntConsumer)
        {
            Random rnd = new Random();
            buffer = new List<int>();
            commonLocker = new TTASLock();

            Producers = new List<Producer<int>>();
            for (int i = 0; i < cntProducer; i++)
            {
                Producers.Add(new Producer<int>(buffer, commonLocker, rnd.Next));
            }

            Consumers = new List<Consumer<int>>();
            for (int i = 0; i < cntConsumer; i++)
            {
                Consumers.Add(new Consumer<int>(buffer, commonLocker, item => { Thread.Sleep(item % 100); }));
            }
        }

        public void Start()
        {
            Producers.ForEach(thread => thread.Start());
            Consumers.ForEach(thread => thread.Start());
        }

        public void Stop()
        {
            Producers.ForEach(thread => thread.Stop());
            Consumers.ForEach(thread => thread.Stop());
        }
    }
}
