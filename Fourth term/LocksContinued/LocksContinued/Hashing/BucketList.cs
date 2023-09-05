using System;

namespace LocksContinued.Hashing
{
    public class BucketList<T>
    {
        const int HI_MASK = 0x40000000;
        const int MASK = 0x3FFFFFFF;
        AtomicNode<T> head;
        public BucketList()
        {
            head = new AtomicNode<T>()
            {
                Key = 0,
                CurrentMarkedAndNext =
                    new AtomicMarkableReference<AtomicNode<T>>(new AtomicNode<T>() { Key = int.MaxValue }, false)
            };
        }

        public BucketList(AtomicNode<T> head)
        {
            this.head = head;
        }

        public int MakeOrdinaryKey(T x)
        {
            int code = Math.Abs(x.GetHashCode()) & MASK; // take lowest 30 bits
            return Reverse(code | HI_MASK);
        }

        private int MakeSentinelKey(int key)
        {
            return Reverse(key & MASK);
        }

        public bool Contains(T x)
        {
            int key = MakeOrdinaryKey(x);
            Window<T> window = Find(head, key);
            AtomicNode<T> curr = window.Curr;
            return (curr.Key == key);
        }

        private int Reverse(int v)
        {
            // bit reversing method
            return v;
        }

        public BucketList<T> GetSentinel(int index)
        {
            int key = MakeSentinelKey(index);
            bool splice;
            while (true)
            {
                Window<T> window = Find(head, key);
                AtomicNode<T> pred = window.Pred;
                AtomicNode<T> curr = window.Curr;
                if (curr.Key == key)
                {
                    return new BucketList<T>(curr);
                }
                else
                {
                    AtomicNode<T> node = new AtomicNode<T>() { Key = key };
                    node.CurrentMarkedAndNext.Set(pred.CurrentMarkedAndNext.GetReference(), false);
                    splice = pred.CurrentMarkedAndNext.CompareAndSet(curr, node, false, false);
                    if (splice)
                        return new BucketList<T>(node);
                    else
                        continue;
                }
            }
        }

        public bool Add<T>(T x)
        {
            // the copy of lock-free list add
            return true;
        }

        private Window<T> Find(AtomicNode<T> head, int key)
        {
            // the copy of old find
            return null;
        }
    }
}