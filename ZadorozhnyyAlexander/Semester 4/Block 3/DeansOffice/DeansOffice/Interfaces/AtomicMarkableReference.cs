namespace DeansOffice
{
    public class AtomicMarkableReference<T> : IAtomicMarkableReference<T>
    {
        private volatile ReferenceMarkPair<T> pair;

        public AtomicMarkableReference(T initialRef)
        {
            pair = new ReferenceMarkPair<T>(initialRef, false);
        }

        public AtomicMarkableReference(T initialRef, bool initialMark)
        {
            pair = new ReferenceMarkPair<T>(initialRef, initialMark);
        }

        public bool CompareAndSet(T expectedReference, T newReference, bool expectedMark, bool newMark)
        {
            ReferenceMarkPair<T> currentPair = pair;
            if (currentPair.Reference.Equals(expectedReference) && currentPair.Mark == expectedMark)
            {
                ReferenceMarkPair<T> newPair = new ReferenceMarkPair<T>(newReference, newMark);
                return Interlocked.CompareExchange(ref pair, newPair, currentPair) == currentPair;
            }
            return false;
        }

        public T Get(out bool marked)
        {
            marked = pair.Mark;
            return pair.Reference;
        }

        public T GetReference()
        {
            return pair.Reference;
        }

        public void Set(T newReference, bool newMark)
        {
            ReferenceMarkPair<T> currentPair = pair;
            if (!newReference.Equals(currentPair.Reference) || newMark != currentPair.Mark)
            {
                Interlocked.Exchange(ref pair, new ReferenceMarkPair<T>(newReference, newMark));
            }
        }
    }
}