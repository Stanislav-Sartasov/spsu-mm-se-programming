namespace ProducerConsumer;

public class Consumer<T>
{
    private readonly Mutex mutex;
    private readonly Buffer<T> buffer;
    private readonly Action<T> consume;
    private volatile bool isStopped;

    public Consumer(Mutex mutex, Buffer<T> buffer, Action<T> consume)
    {
        this.mutex = mutex;
        this.buffer = buffer;
        this.consume = consume;
    }

    public void Run()
    {
        while (!isStopped)
        {
            mutex.WaitOne();
            if (buffer.IsEmpty)
            {
                mutex.ReleaseMutex();
                continue;
            }
            var item = buffer.Pop();
            mutex.ReleaseMutex();

            consume(item);

            Thread.Sleep(10);
        }
    }

    public void Stop()
    {
        isStopped = true;
    }
}