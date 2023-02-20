namespace LocksContinued.WorkStealing
{
    internal class AtomicStampedReference<T>
    {
        public AtomicStampedReference(T value, int stamp)
        {
        }

        public bool CompareAndSet(T expectedReference, T newReference, int expectedStamp, int newStamp) { return false; }

        public T Get(out int stampHolder) { stampHolder = 0; return default(T); }

        public T GetReference() { return default(T); }

        public void Set(T newReference, int newStamp) { }
    }
}