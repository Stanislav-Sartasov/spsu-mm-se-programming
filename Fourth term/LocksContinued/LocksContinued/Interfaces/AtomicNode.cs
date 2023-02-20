namespace LocksContinued
{
    public class AtomicNode<T>
    {
        public T Value;
        public int Key;
        public IAtomicMarkableReference<AtomicNode<T>> CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(null);
    }
}