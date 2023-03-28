namespace ProducerConsumer;

public class Consumer : ProducerOrConsumer
{
    public Consumer(Mutex mutex, List<Application> items)
    {
        thread = new Thread(Consume);
        Id = thread.ManagedThreadId;
        this.mutex = mutex;
        this.items = items;
    }

    private void Consume()
    {
        int counter = 0;
        var random = new Random();
        
        while (!isStopped)
        {
            var squareMeters = random.Next(10, 1000);
            var type = random.Next(5);
            var date = DateTime.Now.AddDays(random.Next(7));

            mutex.WaitOne();
            items.Add(new Application(squareMeters, ConverIntToCoverType(type), date));
            if (consoleLogging)
                Console.WriteLine($"({Id}): consumer change applications' number from {items.Count - 1} to {items.Count}");
            mutex.ReleaseMutex();

            if (++counter % 2 != 0) continue;
            counter = 0;
            Thread.Sleep(millisecondsTimeout);
        }
    }

    private CoverType ConverIntToCoverType(int i)
    {
        switch (i)
        {
            case 0:
                return CoverType.Parquet;
            case 1:
                return CoverType.Laminate;
            case 2:
                return CoverType.Paint;
            case 3:
                return CoverType.Tile;
            case 4:
                return CoverType.Wallpaper;
            default:
                return CoverType.LiquidWallpaper;
        }
    }
}
