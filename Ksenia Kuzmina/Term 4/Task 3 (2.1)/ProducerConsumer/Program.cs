namespace ProducerConsumer
{
    public class Program
    {
        private static List<int> buffer = new List<int>();
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private static Semaphore producerSemaphore = new Semaphore(1, 1);
        private static Semaphore consumerSemaphore = new Semaphore(0, 1);

        public static void Main(string[] args)
        {
            int numProducer = GetConsoleReadline("producers");
            int numConsumers = GetConsoleReadline("consumers");

            Producer producer = new Producer(cancellationTokenSource, buffer, producerSemaphore, consumerSemaphore, numProducer);
            Consumer consumer = new Consumer(consumerSemaphore, producerSemaphore, buffer, cancellationTokenSource, numConsumers);

            producer.Start();
            consumer.Start();

            Console.ReadKey(true);

            producer.Stop();
            consumer.Stop();
        }

        public static int GetConsoleReadline(string type)
        {
            do
            {
                try
                {
                    Console.Write("Enter the number of " + type + ": ");
                    int num = int.Parse(Console.ReadLine());

                    if (num > 0) { return num; }

                    Console.WriteLine("Enter a positive number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            } while (true);
        }
    }
}