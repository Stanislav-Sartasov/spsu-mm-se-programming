namespace ProducerConsumer;

public class Factory : IDisposable
{
    private readonly int producerNumber;
    private readonly int consumerNumber;
    
    private List<ProducerOrConsumer> marketParticipant = new();

    private List<Object> items = new();
    private Mutex mutex = new();
    private bool running;
    
    public Factory(int producerNumber, int consumerNumber)
    {
        this.producerNumber = producerNumber;
        this.consumerNumber = consumerNumber;
    }

    public void Start()
    {
        if (running)
        {
            throw new InvalidOperationException("You can not start the factory, while it's running!");
        }

        running = true;

        for (var i = 0; i < consumerNumber; i++)
        {
            var consumer = new Consumer(mutex, items); 
            marketParticipant.Add(consumer);
            consumer.Start();
        }
        
        for (var i = 0; i < producerNumber; i++)
        {
            var producer = new Producer(mutex, items);
            marketParticipant.Add(producer);
            producer.Start();
        }
    }

    public static void EnableConsoleLogs()
    {
        ProducerOrConsumer.UseConsoleLogging();
    }

    public static void DisableConsoleLogs()
    {
        ProducerOrConsumer.DisableConsoleLogging();
    }

    public static void UpdateTimeout(int millisecondsTimeout)
    {
        ProducerOrConsumer.UpdateTimeout(millisecondsTimeout);
    }

    public void Stop()
    {
        foreach (var participant in marketParticipant)
        {
            participant.Stop();
        }

        running = false;
    }

    public void Dispose()
    {
        mutex.Dispose();
    }
}
