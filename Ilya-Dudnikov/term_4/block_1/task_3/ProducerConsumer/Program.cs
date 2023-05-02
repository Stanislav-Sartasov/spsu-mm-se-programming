namespace ProducerConsumer;

public class Program
{
    private const int NumberOfConsumers = 5;
    private const int NumberOfProducers = 5;

    public static void Main(string[] args)
    {
        var manager = new ProducerConsumerManager(NumberOfConsumers, NumberOfProducers);

        Console.WriteLine("Running...");
        manager.Run();

        Console.ReadKey(true);
        manager.Stop();

        Console.WriteLine("Stopped!");
    }
}