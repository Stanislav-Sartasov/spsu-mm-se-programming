namespace ProducerConsumer.Consumer;

public class Consumer<T> : IConsumer
{
    private const int Timeout = 10;
    private readonly List<T> buffer;
    private readonly Action<T> consume;
    private readonly Mutex mutex;
    private volatile bool stopFlag;

    public Consumer(Mutex mutex, List<T> buffer, Action<T> consume)
    {
        this.mutex = mutex;
        this.buffer = buffer;
        this.consume = consume;
    }

    public void Run()
    {
        while (!stopFlag)
        {
            mutex.WaitOne();

            if (!buffer.Any())
            {
                mutex.ReleaseMutex();
                continue;
            }

            var consumedItem = buffer[0];
            buffer.RemoveAt(0);
            mutex.ReleaseMutex();

            if (consumedItem != null)
                consume(consumedItem);
        }
    }

    public void Stop()
    {
        stopFlag = true;
    }
}