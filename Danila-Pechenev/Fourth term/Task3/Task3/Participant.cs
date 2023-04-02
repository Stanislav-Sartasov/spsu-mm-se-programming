namespace Task3;

public abstract class Participant
{
    protected static Random random = new Random();
    protected Thread thread;
    protected Semaphore semaphore;
    protected int maxObjectsNumber;
    protected int pauseBetweenActions;
    protected List<int> numbers;

    public Participant(Semaphore semaphore, int maxObjectsNumber, int pauseBetweenActions, List<int> numbers)
    {
        thread = new Thread(Act);
        this.semaphore = semaphore;
        this.maxObjectsNumber = maxObjectsNumber;
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
        thread.Join();
    }
}
