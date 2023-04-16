namespace ConsumerProducer.Locks
{
    public interface ILock
    {
        public void Lock();
        public void Unlock();
    }
}
