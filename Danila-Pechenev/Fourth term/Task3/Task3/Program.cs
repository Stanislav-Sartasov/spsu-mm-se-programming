namespace Task3;

class Program
{
    static int Main(string[] args)
    {
        // Determine number of producers and consumers:
        int producersNumber = 4;
        int consumersNumber = 4;

        int pauseBetweenActionsMax = 10;

        var factory = new Factory(producersNumber, consumersNumber, pauseBetweenActionsMax);

        factory.Start();
        Console.ReadKey(true);
        factory.Stop();

        return 0;
    }
}
