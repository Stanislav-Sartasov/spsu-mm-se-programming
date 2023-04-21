namespace ProducerConsumer
{
    public class Manager<T>
    {
        List<T> applications;

        List<Consumer<T>> consumers;
        List<Producer<T>> producers;

        Random pauseGenerator;

        Semaphore semaphore;

        public Manager(int numberOfConsumers, int numberOfProducers, int maxPauseTime, Func<T> produceFunc)
        {
            pauseGenerator = new Random();
            semaphore = new Semaphore(1, 1);
            applications = new List<T>();

            consumers = (from _ in Enumerable.Range(0, numberOfConsumers) select new Consumer<T>(semaphore, pauseGenerator.Next(1, maxPauseTime + 1), applications)).ToList();
            producers = (from _ in Enumerable.Range(0, numberOfProducers) select new Producer<T>(semaphore, pauseGenerator.Next(1, maxPauseTime + 1), applications, produceFunc)).ToList();

            Console.WriteLine("Manager is ready!");
        }

        public void Work()
        {
            StartWork();
            Console.ReadKey(true);
            StopWork();
        }

        public void StartWork()
        {
            Console.WriteLine("The work was started.");

            foreach (var consumer in consumers)
                consumer.Start();

            foreach (var producer in producers)
                producer.Start();
        }

        public void StopWork()
        {
            foreach (var consumer in consumers)
                consumer.Stop();

            foreach (var producer in producers)
                producer.Stop();

            Console.WriteLine("The work was done.");
        }
    }
}
