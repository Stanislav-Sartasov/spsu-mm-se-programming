using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Hashing
{
    public class StripedCuckooHashSet<T> : PhasedCuckooHashSet<T>
    {
        Mutex[,] locks;

        public StripedCuckooHashSet(int capacity) : base(capacity)
        {
            locks = new Mutex[2, capacity];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < capacity; j++)
                {
                    locks[i, j] = new Mutex();
                }
            }
        }


        protected override void Acquire(T x)
        {
            locks[0, Hash0(x) % locks.GetLength(1)].WaitOne();
            locks[1, Hash1(x) % locks.GetLength(1)].WaitOne();
        }

        protected override void Release(T x)
        {
            locks[0, Hash0(x) % locks.GetLength(1)].ReleaseMutex();
            locks[1, Hash1(x) % locks.GetLength(1)].ReleaseMutex();
        }

        protected override void Resize()
        {
            int oldCapacity = capacity;
            for (int i = 0; i < oldCapacity; i++)
            {
                locks[0, i].WaitOne();
            }

            try
            {
                if (capacity != oldCapacity)
                {
                    return;
                }
                List<T>[,] oldTable = table;
                capacity = 2 * capacity;
                table = new List<T>[2, capacity];

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
                for (int i = 0; i < oldCapacity; i++)
                {
                    locks[0, i].ReleaseMutex();
                }
            }
        }
    }
}
