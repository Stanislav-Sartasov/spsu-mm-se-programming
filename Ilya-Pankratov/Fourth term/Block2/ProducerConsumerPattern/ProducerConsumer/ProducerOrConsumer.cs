namespace ProducerConsumer;

public abstract class ProducerOrConsumer
{
    public int Id { get; protected set; }
    
    protected Thread thread;
    protected Mutex mutex;
    protected List<Object> items;
    protected volatile bool isStopped;
    protected static volatile bool consoleLogging;
    protected static volatile int millisecondsTimeout;

    public void Start()
    {
        thread.Start();
    }

    public void Stop()
    {
        isStopped = true;
    }

    public static void UseConsoleLogging()
    {
        consoleLogging = true;
    }

    public static void DisableConsoleLogging()
    {
        consoleLogging = false;
    }

    public static void UpdateTimeout(int millisecondsTimeout)
    {
        ProducerOrConsumer.millisecondsTimeout = millisecondsTimeout;
    } 
}
