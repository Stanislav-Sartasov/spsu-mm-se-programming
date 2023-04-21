namespace ProducerConsumer;

public class Producer<T>
{
    private readonly Mutex mutex;
    private readonly Buffer<T> buffer;
    private readonly Func<T> produce;
    private volatile bool isStopped;

    public Producer(Mutex mutex, Buffer<T> buffer, Func<T> produce)
    {
        this.mutex = mutex;
        this.buffer = buffer;
        this.produce = produce;
    }

    public void Run()
    {
        while (!isStopped)
        {
            var item = produce();

            mutex.WaitOne();
            buffer.Push(item);
            mutex.ReleaseMutex();

            Thread.Sleep(10);
        }
    }

    public void Stop()
    {
        isStopped = true;
    }
}