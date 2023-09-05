using System;

namespace LocksContinued
{
    public class LockFreeSet<T>
    {
        private AtomicNode<T> _tail = new AtomicNode<T>();
        private AtomicNode<T> _head = new AtomicNode<T>();

        public LockFreeSet()
        {
            _head.CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(_tail);
        }

        public bool Add(T item)
        {
            int key = item.GetHashCode();
            while (true)
            {
                //Tuple<AtomicNode<T>, AtomicNode<T>>
                var window = Find(_head, key);
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
                var window = Find(_head, key);
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
                    return true;
                }
            }
        }

        public bool Contains(T item)
        {
            bool marked = false;
            int key = item.GetHashCode();
            AtomicNode<T> curr = _head;
            while (curr.Key < key)
            {
                curr = curr.CurrentMarkedAndNext.GetReference();
                AtomicNode<T> succ = curr.CurrentMarkedAndNext.Get(out marked);
            }
            return (curr.Key == key && !marked);
        }

        private (AtomicNode<T>, AtomicNode<T>) Find(AtomicNode<T> head, int key)
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