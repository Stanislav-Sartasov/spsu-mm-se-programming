namespace Task3;

public abstract class Participant
{
    protected static Random random = new Random();
    protected volatile bool stop = false;
    protected Thread thread;
    protected Semaphore semaphore;
    protected int maxObjectsNumber;
    protected int pauseBetweenActions;
    protected List<int> numbers;

    public Participant(Semaphore semaphore, int pauseBetweenActions, List<int> numbers)
    {
        thread = new Thread(Act);
        this.semaphore = semaphore;
        this.pauseBetweenActions = pauseBetweenActions;
        this.numbers = numbers;
    }

    protected abstract void Act();

    public void StartAction()
    {
        thread.Start();
    }

    public void StopAction()
    {
        stop = true;
        thread.Join();
    }
}
