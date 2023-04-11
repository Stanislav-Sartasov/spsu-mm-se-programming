using System.Security.Cryptography.X509Certificates;

namespace ProducerConsumer;

internal class Program
{
    private static int ProduceFunc()
    {
        // work emulation
        Thread.Sleep(100);
        return 0;
    }

    private static void ConsumeAction(int item)
    {
        // work emulation
        Thread.Sleep(100);
    }

    private const int ProducersCount = 4;
    private const int ConsumersCount = 4;

    public static void Main(string[] args)
    {
        var manager = new Manager<int>(ProducersCount, ConsumersCount, ProduceFunc, ConsumeAction);

        manager.Start();

        Console.ReadKey(true);

        manager.Stop();
        manager.Dispose();
    }
}
