namespace LocksContinued
{
    public class AtomicMarkableReference<T>: IAtomicMarkableReference<T>
    {
        public AtomicMarkableReference(T initialValue)
        {
            
        }

        public AtomicMarkableReference(T initialValue, bool initialMark)
        {

        }
        public bool CompareAndSet(T expectedReference, T newReference, bool expectedMark, bool newMark)
        {
            return true;
        }

        public bool AttemptMark(T expectedReference, bool newMark)
        {
            return true;
        }

        public T Get(out bool marked)
        {
            marked = true;
            return default(T);
        }

        public T GetReference()
        {
            return default(T);
        }

        public void Set(T newReference, bool newMark)
        {
            
        }
    }
}