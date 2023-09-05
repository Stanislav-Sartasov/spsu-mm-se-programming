using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.AtomicTypes;

namespace Task5.ExamSystem
{
    public class ExamSet : IExamSystem
    {
        private AtomicNode<Tuple<long, long>> _tail = new AtomicNode<Tuple<long, long>> { Key = int.MaxValue };
        private AtomicNode<Tuple<long, long>> _head = new AtomicNode<Tuple<long, long>> { Key = int.MinValue };
        volatile private int _size = 0;

        public ExamSet()
        {
            _head.CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<Tuple<long, long>>>(_tail, false);
        }

        public void Add(long studentId, long courseId)
        {
            Tuple<long, long> credit = new Tuple<long, long>(studentId, courseId);
            int key = credit.GetHashCode();
            while (true)
            {
                //Tuple<AtomicNode<T>, AtomicNode<T>>
                var window = Find(_head, key);
                AtomicNode<Tuple<long, long>> pred = window.Item1, curr = window.Item2;
                if (curr.Key == key && credit.Equals(curr.Value))
                {
                    return; // false;
                }
                else
                {
                    AtomicNode<Tuple<long, long>> node = new AtomicNode<Tuple<long, long>>()
                    {
                        Key = key,
                        Value = credit,
                        CurrentMarkedAndNext = new AtomicMarkableReference<AtomicNode<Tuple<long, long>>>(curr, false)
                    };
                    if (pred.CurrentMarkedAndNext.CompareAndSet(curr, node, false, false))
                    {
                        Interlocked.Increment(ref _size);
                        return; // true;
                    }
                }
            }
        }

        public void Remove(long studentId, long courseId)
        {
            Tuple<long, long> credit = new Tuple<long, long>(studentId, courseId);
            int key = credit.GetHashCode();
            bool snip;
            while (true)
            {
                var window = Find(_head, key);
                AtomicNode<Tuple<long, long>> pred = window.Item1, curr = window.Item2;
                if (curr.Key != key)
                {
                    return;// false;
                }
                else
                {
                    AtomicNode<Tuple<long, long>> succ = curr.CurrentMarkedAndNext.GetReference();
                    snip = curr.CurrentMarkedAndNext.CompareAndSet(succ, succ, false, true);
                    if (!snip)
                        continue;
                    pred.CurrentMarkedAndNext.CompareAndSet(curr, succ, false, false);
                    Interlocked.Decrement(ref _size);
                    return;// true;
                }
            }
        }

        public bool Contains(long studentId, long courseId)
        {
            Tuple<long, long> credit = new Tuple<long, long>(studentId, courseId);
            int key = credit.GetHashCode();
            bool marked = false;
            AtomicNode<Tuple<long, long>> curr = _head;
            while (curr.Key < key)
            {
                curr = curr.CurrentMarkedAndNext.GetReference();
                AtomicNode<Tuple<long, long>> succ = curr.CurrentMarkedAndNext.Get(out marked);
            }
            return curr.Key == key && !marked;
        }

        public int Count { get { return _size; } }

        private (AtomicNode<Tuple<long, long>>, AtomicNode<Tuple<long, long>>) Find(AtomicNode<Tuple<long, long>> head, int key)
        {
            AtomicNode<Tuple<long, long>> pred = null, curr = null, succ = null;
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
