namespace Task3;

class Program
{
    static void Main(string[] args)
    {
        ProducerConsumerManager manager = new ProducerConsumerManager(3, 3);
        Console.WriteLine("Run");
        manager.Start();

        Console.ReadKey(true);
        manager.Stop();
        Console.WriteLine("Stop");
    }
}