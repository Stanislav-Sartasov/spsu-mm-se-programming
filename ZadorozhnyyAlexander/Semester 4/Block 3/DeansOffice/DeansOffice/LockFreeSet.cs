namespace DeansOffice
{
    public class LockFreeSet<T>
    {
        private AtomicNode<T> tail = new AtomicNode<T> { Key = int.MaxValue };
        private AtomicNode<T> head = new AtomicNode<T> { Key = int.MinValue };

        private volatile int count = 0;

        public LockFreeSet()
        {
            head.CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(tail);
        }

        public int Count() => count;

        public bool Add(T item)
        {
            int key = item.GetHashCode();
            while (true)
            {
                //Tuple<AtomicNode<T>, AtomicNode<T>>
                var window = Find(head, key);
                AtomicNode<T> pred = window.Item1, curr = window.Item2;
                if (curr.Key == key)
                {
                    return false;
                }
                else
                {
                    AtomicNode<T> node = new AtomicNode<T>()
                    {
                        Key = key,
                        Value = item,
                        CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(curr, false)
                    };
                    if (pred.CurrentMarkedAndNext.CompareAndSet(curr, node, false, false))
                    {
                        Interlocked.Increment(ref count);
                        return true;
                    }
                }
            }
        }

        public bool Remove(T item)
        {
            int key = item.GetHashCode();
            bool snip;
            while (true)
            {
                var window = Find(head, key);
                AtomicNode<T> pred = window.Item1, curr = window.Item2;
                if (curr.Key != key)
                {
                    return false;
                }
                else
                {
                    AtomicNode<T> succ = curr.CurrentMarkedAndNext.GetReference();
                    snip = curr.CurrentMarkedAndNext.CompareAndSet(succ, succ, false, true);
                    if (!snip)
                        continue;
                    pred.CurrentMarkedAndNext.CompareAndSet(curr, succ, false, false);
                    Interlocked.Decrement(ref count);
                    return true;
                }
            }
        }

        public bool Contains(T item)
        {
            bool marked = false;
            int key = item.GetHashCode();
            AtomicNode<T> curr = head;
            while (curr.Key < key)
            {
                curr = curr.CurrentMarkedAndNext.GetReference();
                AtomicNode<T> succ = curr.CurrentMarkedAndNext.Get(out marked);
            }
            return (curr.Key == key && !marked);
        }

        private (AtomicNode<T>, AtomicNode<T>) Find(AtomicNode<T> head, long key)
        {
            AtomicNode<T> pred = null, curr = null, succ = null;
            bool marked;
            bool snip;

            while (true)
            {
                pred = head;
                curr = pred.CurrentMarkedAndNext.GetReference();
                while (true)
                {
                    bool proceedWithNextCycle = false;

                    succ = curr.CurrentMarkedAndNext.Get(out marked);
                    while (marked)
                    {
                        snip = pred.CurrentMarkedAndNext.CompareAndSet(curr, succ, false, false);
                        if (!snip)
                        {
                            proceedWithNextCycle = true;
                            break;
                        }
                        curr = succ;
                        succ = curr.CurrentMarkedAndNext.Get(out marked);
                    }

                    if (proceedWithNextCycle) break;

                    if (curr.Key >= key)
                        return (pred, curr);

                    pred = curr;
                    curr = succ;
                }
            }
        }
    }
}