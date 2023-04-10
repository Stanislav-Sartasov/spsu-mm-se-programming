using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Manager
    {
        volatile List<int> buffer;
        public List<Producer<int>> producer;
        public List<Consumer<int>> consumer;

        private void consume(int item)
        {
            Thread.Sleep(item % 100);
        }

        public Manager(int cntProducer, int cntConsumer)
        {
            Random rnd = new Random();
            buffer = new List<int>();

            producer = new List<Producer<int>>();
            for (int i = 0; i < cntProducer; i++)
            {
                producer.Add(new Producer<int>(buffer, rnd.Next));
            }

            consumer = new List<Consumer<int>>();
            for (int i = 0; i < cntConsumer; i++)
            {
                consumer.Add(new Consumer<int>(buffer, consume));
            }
        }

        public void Start()
        {
            producer.ForEach(thread => thread.Start());
            consumer.ForEach(thread => thread.Start());
        }

        public void Stop()
        {
            producer.ForEach(thread => thread.Stop());
            consumer.ForEach(thread => thread.Stop());
        }
    }
}
