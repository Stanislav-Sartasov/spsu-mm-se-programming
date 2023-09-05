using System.Text;

namespace DeansOffice
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

        private int Reverse(int n)
        {
            var st = Encoding.ASCII.GetBytes(Convert.ToString(n, 2).PadLeft(31, '0')).Reverse().ToArray();
            int rev = 0;

            for (int i = 0; i < st.Length; i++)
                rev += ((st[st.Length - (i + 1)] & 1) << (i % 32));

            return rev;
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

        public bool Add(T x)
        {
            int key = MakeOrdinaryKey(x);
            while (true)
            {
                //Tuple<AtomicNode<T>, AtomicNode<T>>
                var window = Find(head, key);
                AtomicNode<T> pred = window.Pred, curr = window.Curr;
                if (curr.Key == key)
                {
                    return false;
                }
                else
                {
                    AtomicNode<T> node = new AtomicNode<T>()
                    {
                        Key = key,
                        Value = x,
                        CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<T>>(curr, false)
                    };
                    if (pred.CurrentMarkedAndNext.CompareAndSet(curr, node, false, false))
                    {
                        return true;
                    }
                }
            }
        }

        public bool Remove(T x)
        {
            int key = MakeOrdinaryKey(x);
            bool snip;
            while (true)
            {
                var window = Find(head, key);
                AtomicNode<T> pred = window.Pred, curr = window.Curr;
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

        private Window<T> Find(AtomicNode<T> head, long key)
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
                        return new Window<T>(pred, curr);

                    pred = curr;
                    curr = succ;
                }
            }
        }
    }
}