using System.Collections.Generic;
using System.Threading;

namespace LocksContinued.Hashing
{
    public class RefinableCuckooHashSet<T> : PhasedCuckooHashSet<T>
    {
        AtomicMarkableReference<Thread> owner;
        volatile Mutex[,] locks;
        public RefinableCuckooHashSet(int capacity) : base(capacity)
        {
            locks = new Mutex[2, capacity];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < capacity; j++)
                {
                    locks[i, j] = new Mutex();
                }
            }
            owner = new AtomicMarkableReference<Thread>(null, false);
        }

        protected override void Acquire(T x)
        {
            Thread me = Thread.CurrentThread;
            Thread who;
            while (true)
            {
                bool mark;
                do
                { // wait until not resizing
                    who = owner.Get(out mark);
                } while (mark && who != me);
                Mutex[,] oldLocks = locks;
                Mutex oldLock0 = oldLocks[0, Hash0(x) % oldLocks.GetLength(1)];
                Mutex oldLock1 = oldLocks[1, Hash1(x) % oldLocks.GetLength(1)];
                oldLock0.WaitOne();
                oldLock1.WaitOne();
                who = owner.Get(out mark);
                if ((!mark || who == me) && locks == oldLocks)
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
            locks[0, Hash0(x) % locks.GetLength(1)].ReleaseMutex();
            locks[1, Hash1(x) % locks.GetLength(1)].ReleaseMutex();
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
                    for (int i = 0; i < locks.GetLength(1); i++)
                    {
                        locks[0, i].WaitOne();
                        locks[0, i].ReleaseMutex();
                    }
                    capacity = 2 * capacity;
                    List<T>[,] oldTable = table;
                    table = new List<T>[2, capacity];
                    locks = new Mutex[2, capacity];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < capacity; j++)
                        {
                            locks[i, j] = new Mutex();
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < capacity; j++)
                        {
                            table[i, j] = new List<T>(LIST_SIZE);
                        }
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < capacity; j++)
                        {
                            foreach (T z in oldTable[i, j])
                            {
                                Add(z);
                            }
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
