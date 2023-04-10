namespace ProducerConsumer;

public class Producer : ProducerOrConsumer
{
    public Producer(Mutex mutex, List<Application> items)
    {
        thread = new Thread(ProduceItem);
        Id = thread.ManagedThreadId;
        this.mutex = mutex;
        this.items = items;
    }

    private void ProduceItem()
    {
        int counter = 0;

        while (!isStopped)
        {
            Application? application = null;

            mutex.WaitOne();
            var elements = items.Count;

            if (elements != 0)
            {
                application = items.First();
                items.RemoveAt(0);
            }

            if (consoleLogging)
                Console.WriteLine($"({Id}): producer change applications' number from {0} to {items.Count}",
                    elements == 0 ? 0 : items.Count + 1);
            mutex.ReleaseMutex();

            if (application != null)
            {
                if (application.Time.DayOfWeek == DayOfWeek.Monday)
                    application.Decline();

                if (CalculateCost(application) > 800)
                    application.Approve();
                else
                    application.Decline();
            }

            if (++counter % 2 != 0) continue;
            counter = 0;
            Thread.Sleep(timeout);
        }
    }

    private int CalculateCost(Application application)
    {
        var isDayOff = application.Time.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday;
        var dayOffCoefficient = isDayOff ? 2 : 1;
        var materialCoefficient = ConvertTypeToCostPerSquareMeter(application.CoverType);
        return Convert.ToInt32(application.SquareMeters * dayOffCoefficient * materialCoefficient);
    }

    private double ConvertTypeToCostPerSquareMeter(CoverType type)
    {
        switch (type)
        {
            case CoverType.Parquet:
                return 1.1;
            case CoverType.Laminate:
                return 1.2;
            case CoverType.Paint:
                return 1.3;
            case CoverType.Tile:
                return 1.4;
            case CoverType.Wallpaper:
                return 1.5;
            case CoverType.LiquidWallpaper:
                return 1.6;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}