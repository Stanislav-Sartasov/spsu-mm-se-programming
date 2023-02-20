namespace LocksContinued
{
    internal class Window<T>
    {
        public AtomicNode<T> Pred, Curr;

        public Window(AtomicNode<T> myPred, AtomicNode<T> myCurr)
        {
            Pred = myPred;
            Curr = myCurr;
        }
    }
}