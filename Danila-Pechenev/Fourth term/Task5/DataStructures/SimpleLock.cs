namespace DataStructures;

public class SimpleLock : ILock
{
    private Mutex mutex = new();

    public void Lock()
    {
        mutex.WaitOne();
    }

    public void Unlock()
    {
        mutex.ReleaseMutex();
    }
}