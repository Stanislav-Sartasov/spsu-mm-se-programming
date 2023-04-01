namespace ProducerConsumer.Producer;

public class Producer<T> : IProducer
{
    private const int Timeout = 10;
    private readonly List<T> buffer;
    private readonly Mutex mutex;
    private readonly Func<T> produce;
    private volatile bool stopFlag;

    public Producer(Mutex mutex, List<T> buffer, Func<T> produce)
    {
        this.mutex = mutex;
        this.buffer = buffer;
        this.produce = produce;
    }

    public void Run()
    {
        while (!stopFlag)
        {
            var item = produce();

            mutex.WaitOne();
            buffer.Add(item);
            mutex.ReleaseMutex();
        }

        Thread.Sleep(Timeout);
    }

    public void Stop()
    {
        stopFlag = true;
    }
}