using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocksContinued.Hashing
{
    public class StripedCuckooHashSet<T> : PhasedCuckooHashSet<T>
    {
        ILock[,] _lock;
        public StripedCuckooHashSet(int capacity) : base(capacity)
        {
            _lock = new ILock[2, capacity];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < capacity; j++)
                {
                    _lock[i, j] = new SimpleLock();
                }
            }
        }

        protected override void Acquire(T x)
        {
            _lock[0, Hash0(x) % _lock.GetLength(1)].Lock();
            _lock[1, Hash1(x) % _lock.GetLength(1)].Lock();
        }
        protected override void Release(T x)
        {
            _lock[0, Hash0(x) % _lock.GetLength(1)].Unlock();
            _lock[1, Hash1(x) % _lock.GetLength(1)].Unlock();
        }

        protected override void Resize()
        {
            int oldCapacity = capacity;
            for (int i = 0; i < _lock.GetLength(1); i++)
            {
                _lock[0, i].Lock();
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
                for (int i = 0; i < _lock.GetLength(1); i++)
                {
                    _lock[0, i].Unlock();
                }
            }
        }
    }
}

