namespace ProducerConsumer;

public class Consumer : ProducerOrConsumer
{
    public Consumer(Mutex mutex, List<Object> items)
    {
        thread = new Thread(Consume);
        Id = thread.ManagedThreadId;
        this.mutex = mutex;
        this.items = items;
    }

    private void Consume()
    {
        int counter = 0;
        
        while (!isStopped)
        {
            mutex.WaitOne();
            items.Add(new object());
            if (consoleLogging)
                Console.WriteLine($"({Id}): consumer changes add item from {items.Count - 1} to {items.Count}");
            mutex.ReleaseMutex();

            if (++counter % 2 != 0) continue;
            counter = 0;
            Thread.Sleep(millisecondsTimeout);
        }
    }
}
