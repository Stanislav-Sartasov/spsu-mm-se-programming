using ProducerConsumer;

public static class Program
{
    public static void Main()
    {
        PrintHelloMessage();
        var getProducers = "Enter the number of producers: ";
        var getConsumers = "Enter the number of consumers: ";
        var failMessage = "You are supposed to write integer, try again, please!";

        ReadNumber(getProducers, failMessage, out var producerNumber);
        ReadNumber(getConsumers, failMessage, out var consumerNumber);

        var factory = new Factory(producerNumber, consumerNumber);
        Factory.EnableConsoleLogs();
        Factory.UpdateTimeout(1000);
        factory.Start();
        Console.ReadKey(true);
        factory.Stop();
        factory.Dispose();

        Console.WriteLine("Job done!");
    }

    private static void ReadNumber(string consoleText, string failText,out int number)
    {
        Console.Write(consoleText);
        var input = Console.ReadLine();

        if (!int.TryParse(input, out number))
        {
            while (!int.TryParse(input, out number))
            {
                Console.Write(failText);
                Console.Write(consoleText);
                input = Console.ReadLine();
            }
        }
    }

    private static void PrintHelloMessage()
    {
        Console.WriteLine("Hello! The program implements the producer and consumer pattern with mutex!");
    }
}
