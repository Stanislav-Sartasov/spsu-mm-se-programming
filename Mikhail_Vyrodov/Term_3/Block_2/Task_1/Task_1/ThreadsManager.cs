using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Task_1
{
    class ThreadsManager<T>
    {
        private const int PauseInterval = 100;
        private const int NumProducers = 3;
        private const int NumConsumers = 3;
        private List<T> values = new List<T>();
        private List<Producer<T>> producers = new List<Producer<T>>();
        private List<Consumer<T>> consumers = new List<Consumer<T>>();
        private Func<T> producerFunc;
        private Mutex mut;

        public ThreadsManager(Func<T> producerFunc)
        {
            mut = new Mutex();
            this.producerFunc = producerFunc;
        }

        private void FillLists()
        {
            for (int i = 0; i < NumProducers; i++)
            {
                Producer<T> newProducer = new Producer<T>(PauseInterval, values, producerFunc, mut);
                producers.Add(newProducer);
            }

            for (int i = 0; i < NumConsumers; i++)
            {
                Consumer<T> newConsumer = new Consumer<T>(PauseInterval, values, mut);
                consumers.Add(newConsumer);
            }
            
        }

        public void Start()
        {
            FillLists();

            for (int i = 0; i < NumProducers; i++)
            {
                producers[i].Run();
            }

            for (int i = 0; i < NumConsumers; i++)
            {
                consumers[i].Run();
            }
        }

        public void Stop()
        {
            for (int i = 0; i < NumProducers; i++)
            {
                producers[i].Stop();
            }

            for (int i = 0; i < NumConsumers; i++)
            {
                consumers[i].Stop();
            }

            values.Clear();
            producers.Clear();
            consumers.Clear();
            mut.Dispose();
            Console.WriteLine("Work has finished");
        }
    }
}
