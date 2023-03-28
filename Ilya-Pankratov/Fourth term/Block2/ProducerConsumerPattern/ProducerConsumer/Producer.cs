namespace ProducerConsumer;

public class Producer : ProducerOrConsumer
{
    public Producer(Mutex mutex, List<Object> items)
    {
        thread = new Thread(ProduceItem);
        this.mutex = mutex;
        this.items = items;
    }

    private void ProduceItem()
    {
        int counter = 0;
        
        while (!isStopped)
        {
            mutex.WaitOne();
            var isZeroItems = !items.Any();
            if (!isZeroItems)
                items.RemoveAt(0);
            if (consoleLogging)
                Console.WriteLine($"({Id}): producer changes items from {0} to {items.Count}", isZeroItems ? 0 : items.Count + 1);
            mutex.ReleaseMutex();

            if (++counter % 2 != 0) continue;
            counter = 0;
            Thread.Sleep(millisecondsTimeout);
        }
    }
}
