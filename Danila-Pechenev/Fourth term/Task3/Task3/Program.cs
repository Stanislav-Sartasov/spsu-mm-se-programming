namespace Task3;

class Program
{
    static int Main(string[] args)
    {
        // Determine number of producers and consumers:
        int producersNumber = 4;
        int consumersNumber = 4;

        int maxObjectsNumberMax = 30;
        int pauseBetweenActionsMax = 30;

        var factory = new Factory(producersNumber, consumersNumber, maxObjectsNumberMax, pauseBetweenActionsMax);

        factory.Start();
        Console.ReadKey(true);
        factory.Stop();

        return 0;
    }
}
