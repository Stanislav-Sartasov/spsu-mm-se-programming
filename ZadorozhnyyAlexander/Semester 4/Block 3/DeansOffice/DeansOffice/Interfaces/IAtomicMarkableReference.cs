namespace DeansOffice
{
    public interface IAtomicMarkableReference<T>
    {
        bool CompareAndSet(T expectedReference, T newReference, bool expectedMark, bool newMark);
        T Get(out bool marked);
        T GetReference();
        void Set(T newReference, bool newMark);
    }
}