using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Hashing
{
    public class RefinableCuckooHashSet<T> : PhasedCuckooHashSet<T>
    {
        Mutex[,] _lock;
        AtomicMarkableReference<Thread> owner;
        public RefinableCuckooHashSet(int capacity) : base(capacity)
        {
            _lock = new Mutex[2, capacity];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < capacity; j++)
                {
                    _lock[i, j] = new Mutex();
                }
            }

            owner = new AtomicMarkableReference<Thread>(null, false);
        }

        protected override void Acquire(T x)
        {
            bool mark = true;
            Thread me = Thread.CurrentThread;
            Thread who;
            while (true)
            {
                do
                { // wait until not resizing
                    who = owner.Get(out mark);
                } while (mark && who != me);
                var oldLocks = _lock;
                var oldLock0 = oldLocks[0, Hash0(x) % oldLocks.GetLength(1)];
                var oldLock1 = oldLocks[1, Hash1(x) % oldLocks.GetLength(1)];
                oldLock0.WaitOne();
                oldLock1.WaitOne();
                who = owner.Get(out mark);
                if ((!mark || who == me) && _lock == oldLocks)
                {
                    return;
                }
                else
                {
                    oldLock0.ReleaseMutex();
                    oldLock1.ReleaseMutex();
                }
            }
        }
        protected override void Release(T x)
        {
            _lock[0, Hash0(x)].ReleaseMutex();
            _lock[1, Hash1(x)].ReleaseMutex();
        }

        protected override void Resize()
        {
            int oldCapacity = capacity;
            Thread me = Thread.CurrentThread;
            if (owner.CompareAndSet(null, me, false, true))
            {
                try
                {
                    if (capacity != oldCapacity)
                    { // someone else resized first
                        return;
                    }

                    foreach (Mutex m in _lock)
                    {
                        m.WaitOne();
                        m.ReleaseMutex();
                    }

                    List<T>[,] oldTable = table;
                    capacity = 2 * capacity;
                    table = new List<T>[2, capacity];

                    _lock = new Mutex[2, capacity];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < capacity; j++)
                        {
                            _lock[i, j] = new Mutex();
                        }
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < capacity; j++)
                        {
                            table[i, j] = new List<T>(PROBE_SIZE);
                        }
                    }

                    foreach (List<T> set in oldTable)
                    {
                        foreach (T z in set)
                        {
                            Add(z);
                        }
                    }

                }
                finally
                {
                    owner.Set(null, false);
                }
            }
        }
    }
}
