using ProducerConsumer.Consumer;
using ProducerConsumer.Interfaces;
using ProducerConsumer.Lock;
using ProducerConsumer.Logging;
using ProducerConsumer.Producer;

namespace ProducerConsumer;

public static class Program
{
	public static void Main(string[] args)
	{
		const int producerCount = 10;
		const int consumerCount = 15;

		var buffer = new List<float>();
		var lockObject = new TASLock();
		var logger = new ConsoleLogger();
		var producers = new List<IThread>();
		var consumers = new List<IThread>();

		for (var i = 0; i < producerCount; i++)
			producers.Add(new RandomFloatProducer(lockObject, buffer, logger));

		for (var i = 0; i < consumerCount; i++)
			consumers.Add(new RandomFloatConsumer(lockObject, buffer, logger));

		producers.ForEach(p => p.Start());
		consumers.ForEach(p => p.Start());

		Console.ReadKey();

		logger.Log("Started stopping threads");

		producers.ForEach(p => p.Stop());
		consumers.ForEach(p => p.Stop());
		producers.ForEach(p => p.Wait());
		consumers.ForEach(p => p.Wait());

		logger.Log("Successfully stopped all producers and consumers");
	}
}