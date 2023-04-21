using System.Threading;

namespace LocksContinued
{
    public interface ILock
    {
        void Lock();

        void Unlock();
    }

    public interface IMaybeLock
    {
        bool TryLock(long patienceInMs);

        void Unlock();
    }

    public class SimpleLock:ILock
    {
        private object obj = new object();
        public void Lock()
        {
            Monitor.Enter(obj);
        }

        public void Unlock()
        {
            Monitor.Exit(obj);
        }
    }
}